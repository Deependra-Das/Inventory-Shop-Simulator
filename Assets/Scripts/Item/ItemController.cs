using ServiceLocator.Event;
using ServiceLocator.UI;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemController
    {
        private ItemScriptableObject _itemScriptableObject;
        private ItemView _itemView;
        private Event.EventService _eventService;
        private UIContentPanels _uiPanel;

        public ItemController(ItemScriptableObject itemScriptableObject, Event.EventService eventService, UIContentPanels uiPanel) 
        {
            this._eventService = eventService;
            this._itemScriptableObject = itemScriptableObject;
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
            _eventService.OnItemButtonClickEvent.Invoke(_itemScriptableObject, _uiPanel);

        }


        public string ItemName { get => _itemScriptableObject.itemName; }

        public Sprite ItemIcon { get => _itemScriptableObject.itemIcon; }

        public ItemType ItemType { get => _itemScriptableObject.itemType; }

        public ItemRarity Rarity { get => _itemScriptableObject.rarity; }

        public float BuyingPrice { get => _itemScriptableObject.buyingPrice; }

        public float SellingPrice { get => _itemScriptableObject.sellingPrice; }

        public float Weight { get => _itemScriptableObject.weight; }

        public int Quantity { get => _itemScriptableObject.quantity; }

        public string ItemDescription { get => _itemScriptableObject.itemDescription; }

        public ItemScriptableObject ItemData { get => _itemScriptableObject; }

    }
}