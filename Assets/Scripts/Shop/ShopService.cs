using ServiceLocator.Event;
using ServiceLocator.Item;
using UnityEngine;

namespace ServiceLocator.Shop
{
    public class ShopService
    {
        private ShopScriptableObject _shopData;
        private ShopController shopController;
        public ShopService(ShopScriptableObject shopData, GameObject shopView)
        {
            shopController = new ShopController(_shopData, shopView);

            this._shopData = shopData;

        }
        ~ShopService() { }

        public void InitializeShop(ItemService itemService)
        {
            foreach (var itemData in _shopData.shopItemList)
            {
                shopController.AddNewItemInShop(itemData, itemService);
            }
        }


    }
}
