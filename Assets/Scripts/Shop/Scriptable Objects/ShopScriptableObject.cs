using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Item;

namespace ServiceLocator.Shop
{
    [CreateAssetMenu(fileName = "ShopScriptableObject", menuName = "ScriptableObjects/ShopScriptableObject")]
    public class ShopScriptableObject : ScriptableObject
    {
        public List<ItemScriptableObject> shopItemList;
    }
}