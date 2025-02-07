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
        private ShopScriptableObject _shopCurrentData;

        public ShopService(ShopScriptableObject shopCurrentData) 
        {
            this._shopCurrentData = shopCurrentData;
        }

        ~ShopService() { }

        public void Initialize(ItemDatabaseScriptableObject shopInitialData, ItemService itemService, EventService eventService)
        {
            this._shopInitialData = shopInitialData;
            this._itemService = itemService;
            this._eventService = eventService;

            shopController = new ShopController(_shopCurrentData, _eventService);

            PopulateShopData();
        }

        private void PopulateShopData()
        {
            foreach (var itemData in _shopInitialData.itemDataList)
            {
                shopController.AddNewItemInShop(itemData, _itemService);
            }
        }
    }
}
