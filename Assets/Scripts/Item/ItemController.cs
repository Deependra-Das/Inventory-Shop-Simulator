using ServiceLocator.Event;
using ServiceLocator.UI;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemController
    {
        private ItemWithQuantity _itemDataObj;
        private ItemView _itemView;
        private Event.EventService _eventService;
        private UIContentPanels _uiPanel;

        public ItemController(ItemWithQuantity itemDataObj, Event.EventService eventService, UIContentPanels uiPanel) 
        {
            this._eventService = eventService;
            this._itemDataObj = itemDataObj;
            this._uiPanel = uiPanel;
            _itemView = _eventService.OnCreateItemButtonUIEvent.Invoke<GameObject>(uiPanel).GetComponent<ItemView>();
            _itemView.SetController(this);
            _itemView.SetViewData();
            SetListenerToItemButton();
        }

        ~ItemController() { }

        private void SetListenerToItemButton()
        {
            Button button = _itemView.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => ItemButtonClicked());
            }
            else
            {
                Debug.LogError("Button component missing in ItemView Prefab!");
            }
        }

        private void ItemButtonClicked()
        {
            _eventService.OnItemButtonClickEvent.Invoke(_itemDataObj, _uiPanel);

        }


        public string ItemName { get => _itemDataObj.item.itemName; }

        public Sprite ItemIcon { get => _itemDataObj.item.itemIcon; }

        public ItemType ItemType { get => _itemDataObj.item.itemType; }

        public ItemRarity Rarity { get => _itemDataObj.item.rarity; }

        public float BuyingPrice { get => _itemDataObj.item.buyingPrice; }

        public float SellingPrice { get => _itemDataObj.item.sellingPrice; }

        public float Weight { get => _itemDataObj.item.weight; }

        public int Quantity { get => _itemDataObj.quantity; }

        public string ItemDescription { get => _itemDataObj.item.itemDescription; }

        public ItemWithQuantity ItemData { get => _itemDataObj; }

    }
}