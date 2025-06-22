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
            // Player events
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

        private static readonly Dictionary<(EEventType, Type), object> _eventDict = new();
        // private static readonly Dictionary<EEventType, object>

        public static Event<EEventType, T> GetEvent<T>(EEventType eventType)
        {
            var type = (eventType, typeof(T));
            if (!_eventDict.TryGetValue(type, out var eventObj))
            {
                eventObj = new Event<EEventType, T>();
                _eventDict[type] = eventObj;
            }
            return (Event<EEventType, T>)eventObj;
        }

        public static void ClearEvents()
        {
            _eventDict.Clear();
        }
    }
}