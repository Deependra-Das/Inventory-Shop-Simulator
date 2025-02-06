using ServiceLocator.Event;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemService
    {
        private EventService _eventService;
        private ItemScriptableObject _itemScriptableObject;
        public ItemService() {}

        ~ItemService() { }

        public void Initialize(EventService eventService)
        {
            _eventService = eventService;
        }

        public ItemController CreateItem(ItemScriptableObject itemScriptableObject, UIContentPanels uiPanel)
        {
            this._itemScriptableObject = itemScriptableObject;

            return new ItemController(_itemScriptableObject, _eventService, uiPanel);

        }
    }

}