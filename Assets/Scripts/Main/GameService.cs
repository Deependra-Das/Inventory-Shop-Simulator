using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        public ItemService itemService;

        [SerializeField] private ItemView _itemView;

        [SerializeField] private ItemScriptableObject _ironSword_Item_SO;
        [SerializeField] private ItemScriptableObject _healthPotion_Item_SO;

        [SerializeField] private GameObject _shopView;

        void Start()
        {
            CreateServices();
            InjectDependencies();
        }

        private void CreateServices()
        {
            itemService = new ItemService(_itemView);
        }

        private void InjectDependencies()
        {
            itemService.CreateItem(_ironSword_Item_SO, _shopView);
            itemService.CreateItem(_healthPotion_Item_SO, _shopView);
        }
    }
}
