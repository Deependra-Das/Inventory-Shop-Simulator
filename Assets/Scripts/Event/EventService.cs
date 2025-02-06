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
        //public EventController<Func<ItemScriptableObject, UIContentPanels, ItemController>> OnCreateItemEvent { get; private set; }

        public EventService()
        {
            OnCreateItemButtonUIEvent = new EventController<Func<UIContentPanels, GameObject>>();
          //  OnCreateItemEvent = new EventController<Func<ItemScriptableObject, UIContentPanels, ItemController>>();
        }
    }
}
