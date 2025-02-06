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
        public EventController<Action<ItemScriptableObject, UIContentPanels>> OnItemButtonClickEvent { get; private set; }

        public EventService()
        {
            OnCreateItemButtonUIEvent = new EventController<Func<UIContentPanels, GameObject>>();
            OnItemButtonClickEvent = new EventController<Action<ItemScriptableObject, UIContentPanels>>();
        }
    }
}
