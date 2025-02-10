using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ServiceLocator.Item
{
    public class ItemView : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private Button _itemButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _rarityImage;
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private Sprite _rarityImageVeryCommon;
        [SerializeField] private Sprite _rarityImageCommon;
        [SerializeField] private Sprite _rarityImageRare;
        [SerializeField] private Sprite _rarityImageEpic;
        [SerializeField] private Sprite _rarityImageLegendary;

        private ItemController _itemController;

        public ItemView() { }

        ~ItemView() { }

        public void SetController(ItemController itemController)
        {
            _itemController = itemController;
        }

        public void SetViewData()
        {
            if(_itemController!=null)
            {
                _iconImage.sprite = _itemController.ItemIcon;
                _quantityText.text = _itemController.Quantity.ToString();
                SetRarityImageBackground();
            }
           
        }

        public void ShowItemButtonView()
        {
            gameObject.SetActive(true);
        }

        public void HideItemButtonView()
        {
            gameObject.SetActive(false);
        }

        public void UpdateQuantityView()
        {
            _quantityText.text = _itemController.Quantity.ToString();
        }

        private void SetRarityImageBackground()
        {
            switch(_itemController.Rarity)
            {
                case ItemRarity.VeryCommon:
                    _rarityImage.sprite = _rarityImageVeryCommon;
                    break;
                case ItemRarity.Common:
                    _rarityImage.sprite = _rarityImageCommon;
                    break;
                case ItemRarity.Rare:
                    _rarityImage.sprite = _rarityImageRare;
                    break;
                case ItemRarity.Epic:
                    _rarityImage.sprite = _rarityImageEpic;
                    break;
                case ItemRarity.Legendary:
                    _rarityImage.sprite = _rarityImageLegendary;
                    break;
            }
        }
    }
}