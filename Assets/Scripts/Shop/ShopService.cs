using ServiceLocator.Event;
using ServiceLocator.Item;
using UnityEngine;

namespace ServiceLocator.Shop
{
    public class ShopService
    {
        private ShopController shopController;
        public ShopService(ShopScriptableObject shopCurrentData, GameObject shopView)
        {
            shopController = new ShopController(shopCurrentData, shopView);

        }
        ~ShopService() { }

        public void InitializeShop(ShopScriptableObject shopInitialData, ItemService itemService)
        {
            foreach (var itemData in shopInitialData.shopItemList)
            {
                shopController.AddNewItemInShop(itemData, itemService);
            }
        }


    }
}
