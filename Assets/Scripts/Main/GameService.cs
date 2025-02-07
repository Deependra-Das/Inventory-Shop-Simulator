using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Shop;
using ServiceLocator.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        public ItemService itemService;
        public ShopService shopService;
        public InventoryService inventoryService;
        public UIService uiService;
        public EventService eventService;

        [SerializeField] private ItemView _itemView;

        [SerializeField] private ItemDatabaseScriptableObject _itemDatabase;
        [SerializeField] private ShopScriptableObject _shopCurrentData;
        [SerializeField] private InventoryScriptableObject _inventoryCurrentData;


        [SerializeField] private GameObject _shopView;

        void Start()
        {
            CreateServices();
            InjectDependencies();
        }

        private void CreateServices()
        {
            eventService = new EventService();
            itemService = new ItemService();
            shopService = new ShopService();
            inventoryService = new InventoryService();
        }

        private void InjectDependencies()
        {
            itemService.Initialize(eventService);
            uiService.Initialize(eventService);
            shopService.Initialize(_itemDatabase, itemService, eventService);
            inventoryService.Initialize(_itemDatabase, itemService, eventService);

            uiService.SelectFilterButtonAll();
        }
    }
}
