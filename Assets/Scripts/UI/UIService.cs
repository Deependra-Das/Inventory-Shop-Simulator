using ServiceLocator.Event;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using System;
using ServiceLocator.Inventory;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService _eventService;
        private ShopService _shopService;
        private InventoryService _inventoryService;

        [Header("Item")]
        [SerializeField] private GameObject itemButtonPrefab;

        [Header("Shop")]
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject shopContent;

        [Header("Inventory")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject inventoryContent;
        [SerializeField] private Slider inventoryWeightSlider;
        [SerializeField] private TextMeshProUGUI inventoryCurrentWeightText;
        [SerializeField] private TextMeshProUGUI inventoryMaxWeightText;
        [SerializeField] private GameObject gatherButton;

        [Header("Currency")]
        [SerializeField] private GameObject currencyPanel;
        [SerializeField] private Image currencyImage;
        [SerializeField] private TextMeshProUGUI currencyText;

        [Header("Filters")]
        [SerializeField] private GameObject filterPanel;
        [SerializeField] private GameObject filterContent;
        [SerializeField] private GameObject filterButtonPrefab;

        [Header("ItemDetails")]
        [SerializeField] private GameObject itemDetailsPanel;
        [SerializeField] private Image itemIconImage;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemRarityText;
        [SerializeField] private TextMeshProUGUI itemWeightText;
        [SerializeField] private TextMeshProUGUI itemQuanityInShopText;
        [SerializeField] private TextMeshProUGUI itemQuanityInInventoryText;
        [SerializeField] private TextMeshProUGUI itemBuyingPriceText;
        [SerializeField] private TextMeshProUGUI itemSellingPriceText;

        [Header("ActionBar")]
        [SerializeField] private TextMeshProUGUI actionText;
        [SerializeField] private TextMeshProUGUI currencyAmountText;
        [SerializeField] private GameObject actionButton;
        [SerializeField] private TextMeshProUGUI actionButtonText;

        [Header("Notification")]
        [SerializeField] private GameObject notificationPanel;
        [SerializeField] private GameObject notificationContent;
        [SerializeField] private TextMeshProUGUI notificationTitle;
        [SerializeField] private TextMeshProUGUI notificationMessage;
        [SerializeField] private GameObject notificationButton;

        private List<GameObject> filterButtonList;
        private float _currentInventoryWeight;
        private float _maxInventoryWeight;
        private int _minQuantity;
        private int _maxQuantity;
        private int _currencyAmount;

        public UIService() {}

        public void Initialize(EventService eventService, ShopService shopService, InventoryService inventoryService)
        {
            this._eventService = eventService;
            this._shopService = shopService;
            this._inventoryService = inventoryService;

            itemDetailsPanel.SetActive(false);
            _minQuantity = 0;
            _maxQuantity = 0;
            _currencyAmount = 0;
            _maxInventoryWeight = 0;
            _currentInventoryWeight = 0;

            filterButtonList = new List<GameObject>();
            AddFilterButtons();

            _eventService.OnCreateItemButtonUIEvent.AddListener(CreateItemButtonPrefab);
            _eventService.OnItemButtonClickEvent.AddListener(ShowItemDetails);
            _eventService.OnInventoryWeightUpdateEvent.AddListener(UpdateInventoryWeight);
            gatherButton.gameObject.GetComponent<Button>().onClick.AddListener(GatherButtonClicked);
        }

        ~UIService()
        {
            _eventService.OnCreateItemButtonUIEvent.RemoveListener(CreateItemButtonPrefab);
            _eventService.OnItemButtonClickEvent.RemoveListener(ShowItemDetails);
            _eventService.OnInventoryWeightUpdateEvent.RemoveListener(UpdateInventoryWeight);
        }

        public GameObject CreateItemButtonPrefab(UIContentPanels uiPanel)
        {
            Transform newTransform = GetPanelTransform(uiPanel);
            GameObject newObject = UnityEngine.Object.Instantiate(itemButtonPrefab, newTransform);

            return newObject;
        }

        private Transform GetPanelTransform(UIContentPanels uiPanel)
        {
            switch (uiPanel)
            {
                case UIContentPanels.Inventory:
                    return inventoryContent.transform;
                case UIContentPanels.Shop:
                    return shopContent.transform;
                case UIContentPanels.Filters:
                    return filterContent.transform;
                default:
                    return null;
            }
        }

        private void AddFilterButtons()
        {
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                GameObject filterButton = CreateFilterButtonPrefab(itemType);

                FilterButtonView filterButtonView = filterButton.GetComponent<FilterButtonView>();
                filterButtonView.Initialize(itemType, _eventService);
                filterButton.GetComponentInChildren<TMP_Text>().text = itemType.ToString();
                filterButtonList.Add(filterButton);
            }

        }

        private GameObject CreateFilterButtonPrefab(ItemType itemType)
        {
            Transform newTransform = GetPanelTransform(UIContentPanels.Filters);
            GameObject filterButton = UnityEngine.Object.Instantiate(filterButtonPrefab, newTransform);
            return filterButton;
        }

        public void SelectFilterButtonAll()
        {
            FilterButtonView filterButtonView = filterButtonList[0].GetComponent<FilterButtonView>();
            filterButtonView.ButtonSelectInvoke();
        }

        private void ShowItemDetails(ItemModel itemData, UIContentPanels uiPanel)
        {
          int quantityShop = _shopService.GetQuantityOfItem(itemData);
          int quantityInventory = _inventoryService.GetQuantityOfItem(itemData);

          itemIconImage.sprite = itemData.ItemIcon;
          itemNameText.text = itemData.ItemName;
          itemDescriptionText.text = itemData.ItemDescription;
          itemTypeText.text = itemData.ItemType.ToString();
          itemRarityText.text = itemData.Rarity.ToString();
          itemWeightText.text = itemData.Weight.ToString();
          itemQuanityInShopText.text = quantityShop.ToString();
          itemQuanityInInventoryText.text= quantityInventory.ToString(); ;
          itemBuyingPriceText.text = itemData.BuyingPrice.ToString();
          itemSellingPriceText.text = itemData.SellingPrice.ToString();

          itemDetailsPanel.SetActive(true);
          SetActionBar(itemData, uiPanel, quantityShop, quantityInventory);
        }

        private void GatherButtonClicked()
        {
            _eventService.OnGatherResourcesEvent.Invoke();
        }

        private void UpdateInventoryWeight()
        {
           
            _maxInventoryWeight = _inventoryService.GetMaxInventoryWeight();
            inventoryMaxWeightText.text = " / " + _maxInventoryWeight.ToString() + " lbs";
            inventoryWeightSlider.maxValue = _maxInventoryWeight;

            _currentInventoryWeight = _inventoryService.GetCurrentInventoryWeight();
            inventoryCurrentWeightText.text = _currentInventoryWeight.ToString();
            inventoryWeightSlider.value = _currentInventoryWeight;
           
            UpdateGatherButtonState(_currentInventoryWeight, _maxInventoryWeight);
        }

        private void UpdateGatherButtonState(float currentWeight, float maxWeight)
        {
            if(currentWeight >= maxWeight)
            {
                gatherButton.gameObject.GetComponent<Button>().interactable = false;
                ShowNotification("Inventory Max Weight Limit Reached", "Please sell the items from Inventory before gathering more resources.");
            }
            else
            {
                gatherButton.gameObject.GetComponent<Button>().interactable = true;
            }
        }

        private void ShowNotification(string messageTitle, string messageText)
        {
            notificationTitle.text = messageTitle;
            notificationMessage.text = messageText;
            notificationPanel.SetActive(true);
        }

        private void SetActionBar(ItemModel itemData, UIContentPanels uiPanel, int quantityShop, int quantityInventory)
        {
            if(uiPanel == UIContentPanels.Inventory)
            {
                actionText.text = "Sell "+ itemData.ItemName;
                actionButtonText.text = "Sell";
                currencyAmountText.text = "0";

                _maxQuantity = quantityInventory;
            }
        }

    }
}
