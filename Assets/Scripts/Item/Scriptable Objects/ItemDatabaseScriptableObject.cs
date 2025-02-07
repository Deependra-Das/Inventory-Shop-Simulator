using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Item;

namespace ServiceLocator.Item
{
    [CreateAssetMenu(fileName = "ItemDatabaseScriptableObject", menuName = "ScriptableObjects/ItemDatabaseScriptableObject")]
    public class ItemDatabaseScriptableObject : ScriptableObject
    {
        public List<ItemScriptableObject> itemDataList;
    }
}