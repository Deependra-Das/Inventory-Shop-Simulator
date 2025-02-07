using ServiceLocator.Event;
using ServiceLocator.Item;
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
        private EventService _eventService;
        public void Initialize(ItemType filterItemType, EventService eventService)
        {
            this._filterItemType = filterItemType;
            this._eventService = eventService;
            _filterButton.onClick.AddListener(OnFilterButtonClicked);
            _eventService.OnFilterItemEvent.AddListener(OnFilterUpdateState);

        }

        private void OnFilterButtonClicked()
        {
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
                _filterButtonImage.color = Color.white;
            }
            else
            {
                _filterButtonImage.color = Color.grey;
            }
        }
    }
}

