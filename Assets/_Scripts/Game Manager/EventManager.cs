using System;
using System.Collections.Generic;
using System.Linq;
using Separated.Interfaces;

namespace Separated.GameManager
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, object> _eventDict = new();

        public static Event<T> GetEvent<T>()
        {
            var type = typeof(T);
            if (!_eventDict.TryGetValue(type, out var eventObj))
            {
                eventObj = new Event<T>();
                _eventDict[type] = eventObj;
            }
            return (Event<T>)eventObj;
        }

        public static void ClearEvents()
        {
            _eventDict.Clear();
        }
    }
}