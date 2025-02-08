using System.Collections.Generic;
using ServiceLocator.Item;

namespace ServiceLocator.Shop
{
    public class ShopModel
    {
        private ShopController _shopController;

        private List<ItemController> _shopItemList;

        public ShopModel()
        {
            _shopItemList = new List<ItemController>();
        }

        ~ShopModel() { }

        public void SetController(ShopController shopController)
        {
            _shopController = shopController;
        }

        public void AddItem(ItemController newItem)
        {
            _shopItemList.Add(newItem);
        }

        public void RemoveItem(ItemController item)
        {
            _shopItemList.Remove(item);
        }

        public List<ItemController> ShopItemList { get => _shopItemList; }
    }
}

