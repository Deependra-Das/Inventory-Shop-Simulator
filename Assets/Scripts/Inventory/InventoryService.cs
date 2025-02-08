using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using static UnityEditor.Progress;

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

        public int GetQuantityOfItem(ItemModel item)
        {
            return inventoryController.GetQuantityOfItem(item);
        }

        public float GetCurrentInventoryWeight()
        {
            return inventoryController.GetCurrentInventoryWeight();
        }
        public float GetMaxInventoryWeight()
        {
            return inventoryController.GetMaxInventoryWeight();
        }
    }
}
