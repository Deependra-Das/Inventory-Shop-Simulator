using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Shop;
using ServiceLocator.Inventory;
using ServiceLocator.Currency;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        public ItemService itemService;
        public ShopService shopService;
        public InventoryService inventoryService;
        public UIService uiService;
        public EventService eventService;
        public CurrencyService currencyService;
        public SoundService soundService;

        [SerializeField] private ItemView _itemView;

        [SerializeField] private ItemDatabaseScriptableObject _itemDatabase;
        [SerializeField] private SoundScriptableObject _audioList;

        [SerializeField] private AudioSource SFX_AudioSource;
        [SerializeField] private AudioSource BGM_AudioSource;

        void Start()
        {
            CreateServices();
            InjectDependencies();
            uiService.SelectFilterButtonAll();
        }

        private void CreateServices()
        {
            eventService = new EventService();
            soundService = new SoundService(_audioList, SFX_AudioSource, BGM_AudioSource);
            itemService = new ItemService();
            shopService = new ShopService();
            inventoryService = new InventoryService();
            currencyService = new CurrencyService();
        }

        private void InjectDependencies()
        {
            itemService.Initialize(eventService);
            uiService.Initialize(eventService, shopService, inventoryService, currencyService, soundService);
            shopService.Initialize(_itemDatabase, itemService, eventService);
            inventoryService.Initialize(_itemDatabase, itemService, eventService);
            currencyService.Initialize(eventService);
   
        }
    }
}
