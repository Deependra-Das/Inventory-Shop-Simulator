using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemModel
    {
        private ItemController _itemController;

        public ItemModel(ItemScriptableObject itemDataObj)
        {
            ItemName = itemDataObj.itemName;
            ItemIcon = itemDataObj.itemIcon;
            ItemRarityBackground = null;
            ItemDescription = itemDataObj.itemDescription;
            ItemType = itemDataObj.itemType;
            Rarity = itemDataObj.rarity;
            BuyingPrice = itemDataObj.buyingPrice;
            SellingPrice = itemDataObj.sellingPrice;
            Weight = itemDataObj.weight;
            Quantity = itemDataObj.quantity;
        }

        ~ItemModel() { }

        public void SetController(ItemController itemController)
        {
            _itemController = itemController;
        }


        public string ItemName { get; private set; }

        public Sprite ItemIcon { get; private set; }

        public Sprite ItemRarityBackground { get; private set; }

        public ItemType ItemType { get; private set; }

        public ItemRarity Rarity { get; private set; }

        public float BuyingPrice { get; private set; }

        public float SellingPrice { get; private set; }

        public float Weight { get; private set; }

        public int Quantity { get; private set; }

        public string ItemDescription { get; private set; }

        public void UpdateQuantityData(int newQuantity)
        {
            Quantity = newQuantity;
        }

        public void SetItemRarityBackground(Sprite sprite)
        {
            ItemRarityBackground = sprite;
        }
    }
}


