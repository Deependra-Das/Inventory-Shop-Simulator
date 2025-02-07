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
        private InventoryScriptableObject _inventoryCurrentData;

        public InventoryService(InventoryScriptableObject inventoryCurrentData)
        {
            this._inventoryCurrentData = inventoryCurrentData;
        }

        ~InventoryService() { }

        public void Initialize(ItemDatabaseScriptableObject inventoryInitialData, ItemService itemService, EventService eventService)
        {
            this._inventoryInitialData = inventoryInitialData;
            this._itemService = itemService;
            this._eventService = eventService;

            inventoryController = new InventoryController(_inventoryCurrentData, _eventService);

            PopulateInventoryData();
        }
        private void PopulateInventoryData()
        {
            foreach (var itemData in _inventoryInitialData.itemDataList)
            {
                inventoryController.AddNewItemInInventory(itemData, _itemService);
            }
        }
    }
}
