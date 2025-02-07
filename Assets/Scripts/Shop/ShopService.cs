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
            this._shopInitialData = shopInitialData;
            this._itemService = itemService;
            this._eventService = eventService;

            shopController = new ShopController(_shopInitialData, _eventService, _itemService);

            shopController.PopulateShopData();
        }
    }
}
