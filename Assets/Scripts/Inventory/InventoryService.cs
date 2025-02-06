using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryService
    {
        private InventoryController inventoryController;
        public InventoryService(InventoryScriptableObject inventoryCurrentData)
        {
            inventoryController = new InventoryController(inventoryCurrentData);

        }
        ~InventoryService() { }

        public void Initialize(ItemDatabaseScriptableObject inventoryInitialData, ItemService itemService)
        {
            foreach (var itemData in inventoryInitialData.itemDataList)
            {
                inventoryController.AddNewItemInInventory(itemData, itemService);
            }
        }
    }
}
