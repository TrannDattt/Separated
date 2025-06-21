using System.Collections.Generic;

namespace Separated.Interfaces
{
    public interface IEventListener<T>
    {
        public void OnEventNotify(T eventData);
    }

    public interface IEvent<T>
    {
        public void AddListener(IEventListener<T> listener);
        public void RemoveListener(IEventListener<T> listener);
        public void Notify(T eventData);
    }

    public class Event<T> : IEvent<T>
    {
        private readonly List<IEventListener<T>> _listeners = new();
        public int ListenerCount => _listeners.Count;

        public void AddListener(IEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(IEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }

        public void Notify(T eventData)
        {
            foreach (var listener in _listeners)
            {
                listener.OnEventNotify(eventData);
            }
        }
    }
}