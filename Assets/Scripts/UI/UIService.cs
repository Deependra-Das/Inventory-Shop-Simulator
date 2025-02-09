using ServiceLocator.Event;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using System;
using ServiceLocator.Inventory;
using ServiceLocator.Currency;
using ServiceLocator.Sound;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService _eventService;
        private ShopService _shopService;
        private InventoryService _inventoryService;
        private CurrencyService _currencyService;
        private SoundService _soundService;

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
        [SerializeField] private TextMeshProUGUI notificationTitle;
        [SerializeField] private TextMeshProUGUI notificationMessage;
        [SerializeField] private Button notificationButton;

        [Header("Confirmation")]
        [SerializeField] private GameObject confirmationPanel;
        [SerializeField] private TextMeshProUGUI confirmationMessage;
        [SerializeField] private Button confirmationYesButton;
        [SerializeField] private Button confirmationNoButton;

        private List<GameObject> filterButtonList;
        private float _currentInventoryWeight;
        private float _maxInventoryWeight;
        private int _minQuantity;
        private int _maxQuantity;
        private int _transactionQuantity;
        private float _currencyTransactionAmount;
        private TransactionType _transactionType;
        private ItemModel _itemModelForTransaction;

        public UIService() {}

        public void Initialize(EventService eventService, ShopService shopService, InventoryService inventoryService, CurrencyService currencyService, SoundService soundService)
        {
            this._eventService = eventService;
            this._shopService = shopService;
            this._inventoryService = inventoryService;
            this._currencyService = currencyService;
            this._soundService = soundService;

            itemDetailsPanel.SetActive(false);
            _minQuantity = 0;
            _maxQuantity = 0;
            _transactionQuantity = 0;
            _transactionType = TransactionType.None;
            _maxInventoryWeight = 0;
            _currentInventoryWeight = 0;
            _currencyTransactionAmount = 0f;
            filterButtonList = new List<GameObject>();
            AddFilterButtons();

            _eventService.OnCreateItemButtonUIEvent.AddListener(CreateItemButtonPrefab);
            _eventService.OnItemButtonClickEvent.AddListener(ShowItemDetails);
            _eventService.OnInventoryWeightUpdateEvent.AddListener(UpdateInventoryWeight);
            gatherButton.gameObject.GetComponent<Button>().onClick.AddListener(GatherButtonClicked);
            increaseQuantityButton.onClick.AddListener(IncreaseTransactionQuantity);
            decreaseQuantityButton.onClick.AddListener(DecreaseTransactionQuantity);
            notificationButton.onClick.AddListener(OnNotificationButtonClicked);
            confirmationNoButton.onClick.AddListener(OnConfirmationNoButtonClicked);
            actionButton.onClick.AddListener(OnActionButtonClicked);
        }

        ~UIService()
        {
            _eventService.OnCreateItemButtonUIEvent.RemoveListener(CreateItemButtonPrefab);
            _eventService.OnItemButtonClickEvent.RemoveListener(ShowItemDetails);
            _eventService.OnInventoryWeightUpdateEvent.RemoveListener(UpdateInventoryWeight);
            gatherButton.gameObject.GetComponent<Button>().onClick.RemoveListener(GatherButtonClicked);
            increaseQuantityButton.onClick.RemoveListener(IncreaseTransactionQuantity);
            decreaseQuantityButton.onClick.RemoveListener(DecreaseTransactionQuantity);
            notificationButton.onClick.RemoveListener(OnNotificationButtonClicked);
            confirmationNoButton.onClick.RemoveListener(OnConfirmationNoButtonClicked);
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
                filterButtonView.Initialize(itemType, _eventService, _soundService);
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
            _soundService.PlaySFX(SoundType.ItemClick);
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
            _soundService.PlaySFX(SoundType.GatherResources);
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
                SetUIText(notificationTitle, "Inventory Max Weight Limit Reached");
                SetUIText(notificationMessage, "Please sell the items from Inventory before gathering more resources.");
                ShowNotification();
            }
            else
            {
                gatherButton.gameObject.GetComponent<Button>().interactable = true;
            }
        }

        private void ShowNotification()
        {
            _soundService.PlaySFX(SoundType.Notification);
            notificationPanel.SetActive(true);
        }

        private void SetActionBar(UIContentPanels uiPanel, int quantityShop, int quantityInventory)
        {
            _transactionQuantity = 0;
            _minQuantity = 0;
            confirmationYesButton.onClick.RemoveAllListeners();

            if (uiPanel == UIContentPanels.Inventory)
            {
                _transactionType = TransactionType.Sell;
                SetUIText(actionText, "Sell " + _itemModelForTransaction.ItemName);
                SetUIText(actionButtonText, "Sell ");
                SetUIText(currencyAmountText, "0");

                _maxQuantity = quantityInventory;
                confirmationYesButton.onClick.AddListener(OnSellConfirmButtonClicked);
            }
            else if (uiPanel == UIContentPanels.Shop)
            {
                _transactionType = TransactionType.Buy;
                SetUIText(actionText, "Buy " + _itemModelForTransaction.ItemName);
                SetUIText(actionButtonText, "Buy ");
                SetUIText(currencyAmountText, "0");

                int maxQtyInvWeight = GetMaxQuanityToBuy();
                _maxQuantity = maxQtyInvWeight>quantityShop? quantityShop : maxQtyInvWeight;

                confirmationYesButton.onClick.AddListener(OnBuyConfirmButtonClicked);
            }
            SetupTransaction();
        }

        private void IncreaseTransactionQuantity()
        {
            _soundService.PlaySFX(SoundType.ButtonClick);
            if (_transactionQuantity < _maxQuantity)
            {
                _transactionQuantity++;
                SetupTransaction();
            }
        }

        private void DecreaseTransactionQuantity()
        {
            _soundService.PlaySFX(SoundType.ButtonClick);
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
                _currencyTransactionAmount = _transactionQuantity * _itemModelForTransaction.SellingPrice;
          
            }
            else if(_transactionType==TransactionType.Buy)
            {
                _currencyTransactionAmount = _transactionQuantity * _itemModelForTransaction.BuyingPrice;
            }
            UpdateTransactionText();
        }

        private void UpdateTransactionText()
        {
            SetUIText(transactionQuantityText, _transactionQuantity.ToString());
            SetUIText(currencyAmountText, _currencyTransactionAmount.ToString());
            SetUIText(confirmationMessage, "Do you want to " + _transactionType.ToString() + " " + _transactionQuantity.ToString() + " " + _itemModelForTransaction.ItemName + " ?");
        }


        private int GetMaxQuanityToBuy()
        {
            return (int)((_maxInventoryWeight - _currentInventoryWeight) / _itemModelForTransaction.Weight);
        }

        private void OnNotificationButtonClicked()
        {
            _soundService.PlaySFX(SoundType.ButtonClick);
            notificationPanel.SetActive(false);
        }

        private void OnConfirmationNoButtonClicked()
        {
            _soundService.PlaySFX(SoundType.ButtonClick);
            confirmationPanel.SetActive(false);
            itemDetailsPanel.SetActive(true);
        }
        private void OnActionButtonClicked()
        {

            if(_transactionType == TransactionType.Sell && _transactionQuantity > 0)
            {
                _soundService.PlaySFX(SoundType.ButtonClick);
                itemDetailsPanel.SetActive(false);
                confirmationPanel.SetActive(true);
            }
            else if (_transactionType == TransactionType.Buy && _transactionQuantity > 0 && _currencyService.Currency >0 && _currencyService.Currency >= _currencyTransactionAmount)
            {
                _soundService.PlaySFX(SoundType.ButtonClick);
                itemDetailsPanel.SetActive(false);
                confirmationPanel.SetActive(true);
            }
            else if(_transactionType == TransactionType.Buy && _transactionQuantity >= 0 && (_currencyService.Currency <= 0 || _currencyService.Currency <_currencyTransactionAmount))
            {
                _soundService.PlaySFX(SoundType.ButtonClick);
                itemDetailsPanel.SetActive(false);
                SetUIText(notificationTitle, "Low Currency");
                SetUIText(notificationMessage, "You don't have enough currency to buy the items.");
                ShowNotification();
            }
            else
            {
                _soundService.PlaySFX(SoundType.ButtonDeniedClick);
            }

        }

        private void OnSellConfirmButtonClicked()
        {
            _soundService.PlaySFX(SoundType.ButtonClick);

            bool result1 = _eventService.OnSellItemsInventoryEvent.Invoke<bool>(_itemModelForTransaction.ItemName, _transactionQuantity);
            bool result2 = _eventService.OnSellItemsShopEvent.Invoke<bool>(_itemModelForTransaction.ItemName, _transactionQuantity);
            bool result3 = _eventService.OnSellItemsCurrencyEvent.Invoke<bool>(_itemModelForTransaction.SellingPrice*_transactionQuantity);

            if (result1 && result2 && result3)
            {
                _soundService.PlaySFX(SoundType.CurrencyGain);
                SetUIText(currencyText, _currencyService.Currency.ToString());
                SetUIText(notificationTitle, "Success");
                SetUIText(notificationMessage, _transactionQuantity.ToString() + " " + _itemModelForTransaction.ItemName + " were sold.");
            }
            else
            {
                SetUIText(notificationTitle, "Failure");
                SetUIText(notificationMessage, "The transaction resulted in an error.");
            }
            _transactionType = TransactionType.None;
            ShowNotification();
            confirmationPanel.SetActive(false);
        }

        private void OnBuyConfirmButtonClicked()
        {
            _soundService.PlaySFX(SoundType.ButtonClick);

            bool result1 = _eventService.OnBuyItemsInventoryEvent.Invoke<bool>(_itemModelForTransaction.ItemName, _transactionQuantity);
            bool result2 = _eventService.OnBuyItemsShopEvent.Invoke<bool>(_itemModelForTransaction.ItemName, _transactionQuantity);
            bool result3 = _eventService.OnBuyItemsCurrencyEvent.Invoke<bool>(_itemModelForTransaction.BuyingPrice * _transactionQuantity);

            if (result1 && result2 && result3)
            {
                _soundService.PlaySFX(SoundType.CurrencyLoss);
                SetUIText(currencyText, _currencyService.Currency.ToString());
                SetUIText(notificationTitle, "Success");
                SetUIText(notificationMessage, _transactionQuantity.ToString() + " " + _itemModelForTransaction.ItemName + " were bought.");
            }
            else
            {
                SetUIText(notificationTitle, "Failure");
                SetUIText(notificationMessage, "The transaction resulted in an error.");
            }
            _transactionType = TransactionType.None;
            ShowNotification();
            confirmationPanel.SetActive(false);
        }
        private void SetUIText(TextMeshProUGUI obj, string messageText)
        {
            obj.text = messageText;
        }
    }
}
