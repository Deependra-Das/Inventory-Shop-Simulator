using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class FilterButtonView : MonoBehaviour
    {
        [SerializeField] private Button _filterButton;
        [SerializeField] private Image _filterButtonImage;
        [SerializeField] private ItemType _filterItemType;

        [SerializeField] private Sprite _selectedImage;
        [SerializeField] private Sprite _unselectedImage;

        private EventService _eventService;
        private SoundService _soundService;
        public void Initialize(ItemType filterItemType, EventService eventService, SoundService soundService)
        {
            this._filterItemType = filterItemType;
            this._eventService = eventService;
            this._soundService = soundService;
            _filterButton.onClick.AddListener(OnFilterButtonClicked);
            _eventService.OnFilterItemEvent.AddListener(OnFilterUpdateState);

        }

        private void OnFilterButtonClicked()
        {
            _soundService.PlaySFX(SoundType.ButtonClick);
            _eventService.OnFilterItemEvent.Invoke(_filterItemType);
        }

        public void ButtonSelectInvoke()
        {
            _filterButton.Select();
            _eventService.OnFilterItemEvent.Invoke(_filterItemType);
        }

        private void OnFilterUpdateState(ItemType type)
        {
            if (_filterItemType == type)
            {
                _filterButtonImage.sprite = _selectedImage;
            }
            else
            {
                _filterButtonImage.sprite = _unselectedImage;
            }
        }
    }
}

