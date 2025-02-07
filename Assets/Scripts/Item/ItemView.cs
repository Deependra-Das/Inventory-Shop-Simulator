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
        [SerializeField] private TMP_Text _quantityText;

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
    }
}