using ServiceLocator.Item;
using UnityEngine;
using System.Collections.Generic;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private ShopScriptableObject _shopCurrentData;
        private GameObject _shopView;
        private List<ItemController> itemControllers;

        public ShopController(ShopScriptableObject shopCurrentData, GameObject shopView)
        {
            this._shopCurrentData = shopCurrentData;
            this._shopView = shopView;
           itemControllers = new List<ItemController>();
        }

        ~ShopController() 
        {
            this._shopCurrentData.shopItemList = new List<ItemScriptableObject>();
        }
        public void AddNewItemInShop(ItemScriptableObject itemData, ItemService itemService)
        {
            ItemController itemController = itemService.CreateItem(itemData, _shopView);
            _shopCurrentData.shopItemList.Add(itemController.ItemData);
            itemControllers.Add(itemController);
        }

        public List<ItemController> GetShopItems() => itemControllers;
    }
}