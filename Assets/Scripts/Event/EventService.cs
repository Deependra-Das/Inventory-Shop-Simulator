using System;
using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Item;
using ServiceLocator.UI;

namespace ServiceLocator.Event
{
    public class EventService
    {
        public EventController<Func<UIContentPanels, GameObject>> OnCreateItemButtonUIEvent { get; private set; }
        public EventController<Action<ItemModel, UIContentPanels>> OnItemButtonClickEvent { get; private set; }
        public EventController<Action<ItemType>> OnFilterItemEvent { get; private set; }
        public EventController<Action> OnGatherResourcesEvent { get; private set; }
        public EventController<Action> OnInventoryWeightUpdateEvent { get; private set; }

        public EventService()
        {
            OnCreateItemButtonUIEvent = new EventController<Func<UIContentPanels, GameObject>>();
            OnItemButtonClickEvent = new EventController<Action<ItemModel, UIContentPanels>>();
            OnFilterItemEvent = new EventController<Action<ItemType>>();
            OnGatherResourcesEvent = new EventController<Action>();
            OnInventoryWeightUpdateEvent = new EventController<Action>();
        }
    }
}
