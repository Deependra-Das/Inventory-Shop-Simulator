using ServiceLocator.Event;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using System;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService _eventService;

        [Header("Item")]
        [SerializeField] private GameObject itemButtonPrefab;

        [Header("Shop")]
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject shopContent;

        [Header("Inventory")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject inventoryContent;
        [SerializeField] private Slider inventoryWeightSlider;
        [SerializeField] private TextMeshProUGUI inventoryWeightText;

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

        public UIService() {}

        public void Initialize(EventService eventService)
        {
            this._eventService = eventService;
            _eventService.OnCreateItemButtonUIEvent.AddListener(CreateItemButtonPrefab);
            _eventService.OnItemButtonClickEvent.AddListener(ShowItemDetails);
            itemDetailsPanel.SetActive(false);
            AddFilterButtons();
        }

        ~UIService()
        {
            _eventService.OnCreateItemButtonUIEvent.RemoveListener(CreateItemButtonPrefab);
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
                CreateFilterButtonPrefab(itemType);
            }

        }

        private void CreateFilterButtonPrefab(ItemType itemType)
        {
            Transform newTransform = GetPanelTransform(UIContentPanels.Filters);
            GameObject newObject = UnityEngine.Object.Instantiate(filterButtonPrefab, newTransform);
            newObject.GetComponentInChildren<TMP_Text>().text = itemType.ToString();
        }

        private void ShowItemDetails(ItemWithQuantity itemData, UIContentPanels uiPanel)
        {
          itemDetailsPanel.SetActive(true);
          itemIconImage.sprite = itemData.item.itemIcon;
          itemNameText.text = itemData.item.itemName;
          itemDescriptionText.text = itemData.item.itemDescription;
          itemTypeText.text = itemData.item.itemType.ToString();
          itemRarityText.text = itemData.item.rarity.ToString();
          itemWeightText.text = itemData.item.weight.ToString();
          itemQuanityInShopText.text="TBD";
          itemQuanityInInventoryText.text= "TBD";
          itemBuyingPriceText.text = itemData.item.buyingPrice.ToString();
          itemSellingPriceText.text = itemData.item.sellingPrice.ToString();
        }
    }
}
