using System;
using Separated.Helpers;
using Separated.Views;
using UnityEngine;

namespace Separated.Poolings
{
    public static class UIPooling
    {
        private static Pooling<GameUI> _pool = new();

        public static GameUI GetFromPool(GameUI uiPrefab, Vector2 spawnPos = default, RectTransform parent = null, Action<GameUI> onGet = null)
        {
            var queue = _pool.GetQueue(uiPrefab);

            if (queue.Count == 0)
            {
                var newElement = RuntimeInstanciator<GameUI>.RuntimeInstantiate(uiPrefab);
                queue.Enqueue(newElement);
            }

            var element = queue.Dequeue();
            element.gameObject.SetActive(true);

            var rect = element.GetComponent<RectTransform>();
            rect.SetParent(parent);
            rect.localScale = Vector3.one;
            rect.anchoredPosition = spawnPos;

            onGet?.Invoke(element);
            return element;
        }

        public static void ReturnToPool(GameUI obj)
        {
            obj.gameObject.SetActive(false);
            var queue = _pool.GetQueue(obj);
            queue.Enqueue(obj);
        }
    }
}