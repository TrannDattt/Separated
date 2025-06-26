using System;
using System.Collections.Generic;
using UnityEngine;

namespace Separated.Poolings
{
    public class Pooling<T>
    {
        private Dictionary<Type, Queue<T>> _queueDict = new();

        public Queue<T> GetQueue(T obj)
        {
            var objType = obj.GetType();
            if (_queueDict.ContainsKey(objType))
            {
                return _queueDict[objType];
            }

            var newQueue = new Queue<T>();
            _queueDict.Add(objType, newQueue);
            return newQueue;
        }
    }
}