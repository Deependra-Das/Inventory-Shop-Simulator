using UnityEngine;

namespace ServiceLocator.Item
{
    [CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/ItemScriptableObject")]
    public class ItemScriptableObject : ScriptableObject
    {
        [Header("Item Details")]
        public string itemName;
        public Sprite itemIcon;
        public ItemType itemType;
        public ItemRarity Rarity;
        public float buyingPrice;
        public float sellingPrice;
        public float weight;
        public int Quantity;
        [TextArea] public string itemDescription;

    }
}

