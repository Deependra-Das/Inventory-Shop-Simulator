using ServiceLocator.Event;
using ServiceLocator.Item;
using UnityEngine;

namespace ServiceLocator.Shop
{
    public class ShopService
    {
        private ShopController shopController;
        public ShopService(ShopScriptableObject shopCurrentData)
        {
            shopController = new ShopController(shopCurrentData);

        }
        ~ShopService() { }

        public void Initialize(ShopScriptableObject shopInitialData, ItemService itemService)
        {
            foreach (var itemData in shopInitialData.shopItemList)
            {
                shopController.AddNewItemInShop(itemData, itemService);
            }
        }


    }
}
