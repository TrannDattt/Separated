using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Skills;
using Separated.SummonedBeasts;
using UnityEngine;

namespace Separated.Poolings
{
    public class SummonedObjectPooling
    {
        private static Pooling<ISummonable> _pool = new();

        public static BeastControl GetBeastFromPool(EBeastType type, BeastControl beastPrefab, Vector2 spawnPos = default, Transform parent = null)
        {
            if (type == EBeastType.Null)
            {
                Debug.Log($"Beast is not available.");
                return null;
            }

            var data = BeastDictionary.Instance.GetBeastData(type);
            var queue = _pool.GetQueue(beastPrefab);

            if (queue.Count == 0)
            {
                var newElement = RuntimeInstanciator<BeastControl>.RuntimeInstantiate(beastPrefab);
                queue.Enqueue(newElement);
            }

            var element = queue.Dequeue() as BeastControl;
            element.Initialize(data);
            element.gameObject.SetActive(true);

            element.transform.SetParent(parent);
            element.transform.localScale = Vector3.one;
            element.transform.position = spawnPos;

            return element;
        }

        public static void ReturnObject(ISummonable obj)
        {
            var queue = _pool.GetQueue(obj);
            queue.Enqueue(obj);
        }
    }
}