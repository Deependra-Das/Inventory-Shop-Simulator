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
        [SerializeField] private Button increaseQuantityButton;
        [SerializeField] private Button decreaseQuantityButton;
        [SerializeField] private TextMeshProUGUI transactionQuantityText;
        [SerializeField] private TextMeshProUGUI currencyAmountText;
        [SerializeField] private Button actionButton;
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
        private int _transactionQuantity;
        private float _currencyAmount;
        private TransactionType _transactionType;
        private ItemModel _itemModelForTransaction;

        public UIService() {}

        public void Initialize(EventService eventService, ShopService shopService, InventoryService inventoryService)
        {
            this._eventService = eventService;
            this._shopService = shopService;
            this._inventoryService = inventoryService;

            itemDetailsPanel.SetActive(false);
            _minQuantity = 0;
            _maxQuantity = 0;
            _transactionQuantity = 0;
            _transactionType = TransactionType.None;
            _maxInventoryWeight = 0;
            _currentInventoryWeight = 0;
            _currencyAmount = 0f;
            filterButtonList = new List<GameObject>();
            AddFilterButtons();

            _eventService.OnCreateItemButtonUIEvent.AddListener(CreateItemButtonPrefab);
            _eventService.OnItemButtonClickEvent.AddListener(ShowItemDetails);
            _eventService.OnInventoryWeightUpdateEvent.AddListener(UpdateInventoryWeight);
            gatherButton.gameObject.GetComponent<Button>().onClick.AddListener(GatherButtonClicked);
            increaseQuantityButton.onClick.AddListener(IncreaseTransactionQuantity);
            decreaseQuantityButton.onClick.AddListener(DecreaseTransactionQuantity);
        }

        ~UIService()
        {
            _eventService.OnCreateItemButtonUIEvent.RemoveListener(CreateItemButtonPrefab);
            _eventService.OnItemButtonClickEvent.RemoveListener(ShowItemDetails);
            _eventService.OnInventoryWeightUpdateEvent.RemoveListener(UpdateInventoryWeight);
            gatherButton.gameObject.GetComponent<Button>().onClick.RemoveListener(GatherButtonClicked);
            increaseQuantityButton.onClick.RemoveListener(IncreaseTransactionQuantity);
            decreaseQuantityButton.onClick.RemoveListener(DecreaseTransactionQuantity);
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
          _itemModelForTransaction = itemData;
          SetActionBar(uiPanel, quantityShop, quantityInventory);
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

        private void SetActionBar(UIContentPanels uiPanel, int quantityShop, int quantityInventory)
        {
            if(uiPanel == UIContentPanels.Inventory)
            {
                _transactionType = TransactionType.Sell;
                actionText.text = "Sell "+ _itemModelForTransaction.ItemName;
                actionButtonText.text = "Sell";
                currencyAmountText.text =
                currencyAmountText.text = "0";
                _transactionQuantity = 0;
                _minQuantity = 0;
                _maxQuantity = quantityInventory;
                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(OnSellButtonClicked);
                SetupTransaction();
            }
            else if(uiPanel == UIContentPanels.Shop)
            {
                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(OnBuyButtonClicked);
            }
        }

        private void IncreaseTransactionQuantity()
        {
            if (_transactionQuantity < _maxQuantity)
            {
                _transactionQuantity++;
                SetupTransaction();
            }
        }

        private void DecreaseTransactionQuantity()
        {
            if (_transactionQuantity > _minQuantity)
            {
                _transactionQuantity--;
                SetupTransaction();
            }
        }

        private void SetupTransaction()
        {
            if(_transactionType==TransactionType.Sell)
            {
                _currencyAmount = _transactionQuantity * _itemModelForTransaction.SellingPrice;
          
            }
            else if(_transactionType==TransactionType.Buy)
            {

            }
            UpdateTransactionText();
            UpdateActionButtonState();

        }

        private void UpdateTransactionText()
        {
            transactionQuantityText.text = _transactionQuantity.ToString();
            currencyAmountText.text = _currencyAmount.ToString();
        }

        private void UpdateActionButtonState()
        {
            if (_transactionType==TransactionType.Buy && (_transactionQuantity == 0 || _transactionQuantity * _itemModelForTransaction.Weight >= _maxInventoryWeight - _currentInventoryWeight))
            {
                actionButton.enabled = false;
            }
            else if(_transactionType == TransactionType.Sell && _transactionQuantity == 0)
            {
                actionButton.enabled = false;
            }
            else
            {
                actionButton.enabled = true;
            }
        }

        private void OnSellButtonClicked()
        {
            Debug.Log("Sell");

            bool result1 = _eventService.OnSellItemsInventoryEvent.Invoke<bool>(_itemModelForTransaction.ItemName, _transactionQuantity);
            bool result2 = _eventService.OnSellItemsShopEvent.Invoke<bool>(_itemModelForTransaction.ItemName, _transactionQuantity);

            Debug.Log(result1.ToString()+" "+ result2.ToString());

        }
        private void OnBuyButtonClicked()
        {
            Debug.Log("Buy");
        }
    }
}
