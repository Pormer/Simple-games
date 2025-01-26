using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Code.Score
{
    public abstract class GameEvent{}
    
    
    [CreateAssetMenu(fileName = "EventChannelSO", menuName = "SO/Event/Channel", order = 0)]
    public class EventChannelSO : ScriptableObject
    {
        private Dictionary<Type, Action<GameEvent>> _events = new Dictionary<Type, Action<GameEvent>>();
        private Dictionary<Delegate, Action<GameEvent>> _handlers = new Dictionary<Delegate, Action<GameEvent>>();

        public void AddListener<T>(Action<T> handler) where T : GameEvent
        {
            if(_handlers.ContainsKey(handler)) return;
            
            Action<GameEvent> castAction = e => handler(e as T);
            _handlers[handler] = castAction;
            
            Type evtType = typeof(T);
            if(_events.ContainsKey(evtType)) 
                _events[evtType] += castAction;
            else
                _events[evtType] = castAction;
        }

        public void RemoveListener<T>(Action<T> handler) where T : GameEvent
        {
            Type evtType = typeof(T);
            if (!_events.ContainsKey(evtType)) return;
            
            if (_handlers.TryGetValue(handler, out Action<GameEvent> castAction))
            {
                if (_events.TryGetValue(evtType, out Action<GameEvent> evtAction))
                {
                    evtAction -= castAction;
                    if (evtAction == null)
                        _events.Remove(evtType);
                    else
                        _events[evtType] = evtAction;
                }

                _handlers.Remove(handler);
            }
        }

        public void RaiseEvent(GameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<GameEvent> handler))
            {
                handler?.Invoke(evt);
            }
        }
        
        public void Clear()
        {
            _events.Clear();
            _handlers.Clear();
        }
    }
}