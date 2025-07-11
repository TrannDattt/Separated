using System;
using System.Collections.Generic;
using System.Linq;
using Separated.Interfaces;

namespace Separated.GameManager
{
    public static class EventManager
    {
        public enum EEventType
        {
            // Input events
            GetInput,
            // Player events
            TouchedGround,
            HealthChanged,
            PlayerDied,
            PlayerSkillChanged,
            PlayerSkillUsed,
            // Enemy events
            EnemyDied,
            // Inventory events
            InventoryUpdated,
            // Game events
            GameStateChanged,
            // UI events
            UIUpdated,
            PopMessageDialog,
        }

        private static readonly Dictionary<(EEventType, Type), object> _genericEventDict = new();
        private static readonly Dictionary<EEventType, object> _eventDict = new();

        public static GenericEvent<EEventType, T> GetGenericEvent<T>(EEventType eventType)
        {
            var type = (eventType, typeof(T));
            if (!_genericEventDict.TryGetValue(type, out var eventObj))
            {
                eventObj = new GenericEvent<EEventType, T>();
                _genericEventDict[type] = eventObj;
            }
            return (GenericEvent<EEventType, T>)eventObj;
        }

        public static Event<EEventType> GetEvent(EEventType eventType)
        {
            if (!_eventDict.TryGetValue(eventType, out var eventObj))
            {
                eventObj = new Event<EEventType>();
                _eventDict[eventType] = eventObj;
            }
            return (Event<EEventType>)eventObj;
        }

        public static void ClearGenericEvents()
        {
            _genericEventDict.Clear();
        }

        public static void ClearEvents()
        {
            _eventDict.Clear();
        }
    }
}