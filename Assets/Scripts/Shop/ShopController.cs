using ServiceLocator.Item;
using UnityEngine;
using System.Collections.Generic;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private ShopScriptableObject _shopData;
        private GameObject _shopView;
        private List<ItemController> itemControllers;

        public ShopController(ShopScriptableObject shopData, GameObject shopView)
        {
            this._shopData = shopData;
            this._shopView = shopView;
            itemControllers = new List<ItemController>();
        }

        public void AddNewItemInShop(ItemScriptableObject itemData, ItemService itemService)
        {
            ItemController itemController = itemService.CreateItem(itemData, _shopView);
            itemControllers.Add(itemController);
        }
    }
}