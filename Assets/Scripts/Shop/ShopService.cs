using ServiceLocator.Event;
using ServiceLocator.Item;
using UnityEngine;

namespace ServiceLocator.Shop
{
    public class ShopService
    {
        private ShopController shopController;
        private EventService _eventService;
        private ItemService _itemService;
        private ItemDatabaseScriptableObject _shopInitialData;

        public ShopService() {}

        ~ShopService() { }

        public void Initialize(ItemDatabaseScriptableObject shopInitialData, ItemService itemService, EventService eventService)
        {
            this._itemService = itemService;
            this._eventService = eventService;

            this._shopInitialData = shopInitialData;

            shopController = new ShopController(_shopInitialData, _eventService, _itemService);

            PopulateShopData();
        }

        public int GetQuantityOfItem(ItemModel item)
        {
           return shopController.GetQuantityOfItem(item);
        }

        private void PopulateShopData()
        {
            shopController.PopulateShopData();
        }

    }
}
