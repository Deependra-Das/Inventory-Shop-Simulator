using ServiceLocator.Event;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemService
    {
        private EventService _eventService;
        private ItemWithQuantity _itemDataObj;
        private int _quantity;
        public ItemService() {}

        ~ItemService() { }

        public void Initialize(EventService eventService)
        {
            _eventService = eventService;
        }

        public ItemController CreateItem(ItemWithQuantity itemData, UIContentPanels uiPanel)
        {
            this._itemDataObj = itemData;
            return new ItemController(_itemDataObj, _eventService, uiPanel);

        }
    }

}