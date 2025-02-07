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
        [SerializeField] private ItemType _filterItemType;
        private EventService _eventService;
        public void Initialize(ItemType filterItemType, EventService eventService)
        {
            this._filterItemType = filterItemType;
            this._eventService = eventService;
            _filterButton.onClick.AddListener(OnFilterButtonClicked);

        }

        private void OnFilterButtonClicked()
        {
            _eventService.OnFilterItemEvent.Invoke(_filterItemType);
        }

        public void ButtonSelectInvoke()
        {
            _filterButton.Select();
            _eventService.OnFilterItemEvent.Invoke(_filterItemType);
            Debug.Log(_filterItemType);
        }
    }
}

