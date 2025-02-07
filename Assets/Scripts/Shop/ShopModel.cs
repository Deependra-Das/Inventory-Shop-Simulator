using System.Collections.Generic;
using ServiceLocator.Item;

namespace ServiceLocator.Shop
{
    public class ShopModel
    {
        private ShopController _shopController;

        private List<ItemModel> shopItemList;

        public ShopModel()
        {
            shopItemList = new List<ItemModel>();
        }

        ~ShopModel() { }

        public void SetController(ShopController shopController)
        {
            _shopController = shopController;
        }

        public void AddItem(ItemModel newItem)
        {
            shopItemList.Add(newItem);
        }

        public void RemoveItem(ItemModel item)
        {
            shopItemList.Remove(item);
        }

    }
}

