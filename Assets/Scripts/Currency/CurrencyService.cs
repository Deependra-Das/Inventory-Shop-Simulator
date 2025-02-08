using ServiceLocator.Event;
using System;

namespace ServiceLocator.Currency
{
    public class CurrencyService
    {
        private EventService _eventService;

        public float Currency { get; private set; }
        public CurrencyService() 
        {
            Currency = 0f;
        }
        public void Initialize(EventService eventService)
        {
            this._eventService = eventService;
            _eventService.OnSellItemsCurrencyEvent.AddListener(OnSellItems);
            _eventService.OnBuyItemsCurrencyEvent.AddListener(OnBuyItems);
        }

        ~CurrencyService() 
        {
            _eventService.OnSellItemsCurrencyEvent.RemoveListener(OnSellItems);
            _eventService.OnBuyItemsCurrencyEvent.RemoveListener(OnBuyItems);
        }

        public void AddCurrency(float amount)
        {
            Currency += amount;
            VerifyMinCurrency();
        }

        public void SubtractCurrency(float amount)
        {
            Currency -= amount;
            VerifyMinCurrency();
        }

        public bool OnSellItems(float amount)
        {
            AddCurrency(amount);
            return true;
        }
        public bool OnBuyItems(float amount)
        {
            SubtractCurrency(amount);
            return true;
        }

        private void VerifyMinCurrency()
        {
            if (Currency < 0)
            {
                Currency = 0;
            }
        }
    }

}
