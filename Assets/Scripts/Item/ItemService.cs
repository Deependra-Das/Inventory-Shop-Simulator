using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemService
    {
        private ItemView _itemView;
        private ItemScriptableObject _itemScriptableObject;
        public ItemService(ItemView itemView)
        {
           this._itemView=itemView;
    }

        ~ItemService() { }

        public ItemController CreateItem(ItemScriptableObject itemScriptableObject, GameObject shopView)
        {
            this._itemScriptableObject = itemScriptableObject;

            return new ItemController(_itemScriptableObject, _itemView, shopView);

        }
    }

}