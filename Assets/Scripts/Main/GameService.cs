using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        public ItemService itemService;
        public ShopService shopService;

        [SerializeField] private ItemView _itemView;

        [SerializeField] private ShopScriptableObject _shopCurrentData;
        [SerializeField] private ShopScriptableObject _shopInitialData;

        [SerializeField] private GameObject _shopView;

        void Start()
        {
            CreateServices();
            InjectDependencies();
        }

        private void CreateServices()
        {
            itemService = new ItemService(_itemView);
            shopService = new ShopService(_shopCurrentData, _shopView);
        }

        private void InjectDependencies()
        {
            shopService.InitializeShop(_shopInitialData, itemService);
        }
    }
}
