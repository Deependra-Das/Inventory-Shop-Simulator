using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemController
    {

        private ItemScriptableObject _itemScriptableObject;
        private ItemView _itemView;

        public ItemController(ItemScriptableObject itemScriptableObject, ItemView itemView, GameObject shopView) 
        {
            this._itemScriptableObject = itemScriptableObject;

            _itemView = Object.Instantiate(itemView, shopView.transform);
            _itemView.SetController(this);
            _itemView.SetViewData();
            _itemView.transform.SetParent(shopView.transform);
        }

        ~ItemController() { }

        public string ItemName { get => _itemScriptableObject.itemName; }

        public Sprite ItemIcon { get => _itemScriptableObject.itemIcon; }

        public ItemType ItemType { get => _itemScriptableObject.itemType; }

        public ItemRarity Rarity { get => _itemScriptableObject.rarity; }

        public float BuyingPrice { get => _itemScriptableObject.buyingPrice; }

        public float SellingPrice { get => _itemScriptableObject.sellingPrice; }

        public float Weight { get => _itemScriptableObject.weight; }

        public int Quantity { get => _itemScriptableObject.quantity; }

        public string ItemDescription { get => _itemScriptableObject.itemDescription; }


    }
}