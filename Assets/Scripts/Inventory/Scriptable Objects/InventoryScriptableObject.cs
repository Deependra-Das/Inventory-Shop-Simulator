using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    [CreateAssetMenu(fileName = "InventoryScriptableObject", menuName = "ScriptableObjects/InventoryScriptableObject")]
    public class InventoryScriptableObject : ScriptableObject
    {
        public float currencyOwned;
        public float maxWeight;
        public float currentWeight;
        public List<ItemWithQuantity> inventoryItemList;

    }
}
