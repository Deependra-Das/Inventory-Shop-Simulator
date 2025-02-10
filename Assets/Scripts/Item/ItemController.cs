using ServiceLocator.Event;
using ServiceLocator.UI;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemController
    {
        private ItemModel _itemModel;
        private ItemView _itemView;
        private Event.EventService _eventService;
        private UIContentPanels _uiPanel;

        public ItemController(ItemScriptableObject itemDataObj, Event.EventService eventService, UIContentPanels uiPanel) 
        {
            this._eventService = eventService;
            this._uiPanel = uiPanel;
            _itemModel = new ItemModel(itemDataObj);
            _itemModel.SetController(this);

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
            _eventService.OnItemButtonClickEvent.Invoke(GetItemModel, _uiPanel);

        }

        public void ShowItemButtonView()
        {
            _itemView.ShowItemButtonView();
        }

        public void HideItemButtonView()
        {
            _itemView.HideItemButtonView();
        }

        public string ItemName { get => _itemModel.ItemName; }

        public Sprite ItemIcon { get => _itemModel.ItemIcon; }

        public ItemType ItemType { get => _itemModel.ItemType; }

        public ItemRarity Rarity { get => _itemModel.Rarity; }

        public float BuyingPrice { get => _itemModel.BuyingPrice; }

        public float SellingPrice { get => _itemModel.SellingPrice; }

        public float Weight { get => _itemModel.Weight; }

        public int Quantity { get => _itemModel.Quantity; }

        public string ItemDescription { get => _itemModel.ItemDescription; }

        public ItemModel GetItemModel { get => _itemModel; }

        public void UpdateQuantity(int newQuantity)
        {
           _itemModel.UpdateQuantityData(newQuantity);
            _itemView.UpdateQuantityView();
        }
        public void SetItemRarityBackground(Sprite sprite)
        {
            _itemModel.SetItemRarityBackground(sprite);
        }
    }
}