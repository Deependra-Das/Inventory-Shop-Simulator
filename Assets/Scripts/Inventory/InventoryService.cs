using ServiceLocator.Event;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryService
    {
        private InventoryController inventoryController;
        private EventService _eventService;
        private ItemService _itemService;
        private ItemDatabaseScriptableObject _inventoryInitialData;

        public InventoryService() { }

        ~InventoryService() { }

        public void Initialize(ItemDatabaseScriptableObject inventoryInitialData, ItemService itemService, EventService eventService)
        {
            this._inventoryInitialData = inventoryInitialData;
            this._itemService = itemService;
            this._eventService = eventService;

            inventoryController = new InventoryController(_inventoryInitialData, _eventService, _itemService);

            inventoryController.PopulateInventoryData();
        }
    }
}
