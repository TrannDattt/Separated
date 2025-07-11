using System.Collections.Generic;

namespace Separated.Interfaces
{
    public interface IGenericEventListener<T>
    {
        public void OnEventNotify(T eventData);
    }

    public interface IEventListener
    {
        public void OnEventNotify();
    }

    public interface IGenericEvent<T>
    {
        public void AddListener(IGenericEventListener<T> listener);
        public void RemoveListener(IGenericEventListener<T> listener);
        public void Notify(T eventData);
    }

    public interface IEvent
    {
        public void AddListener(IEventListener listener);
        public void RemoveListener(IEventListener listener);
        public void Notify();
    }

    public class GenericEvent<EEventType, T> : IGenericEvent<T>
    {
        private readonly List<IGenericEventListener<T>> _listeners = new();
        public int ListenerCount => _listeners.Count;

        public void AddListener(IGenericEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(IGenericEventListener<T> listener)
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

    public class Event<EEventType> : IEvent
    {
        private readonly List<IEventListener> _listeners = new();
        public int ListenerCount => _listeners.Count;

        public void AddListener(IEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void Notify()
        {
            foreach (var listener in _listeners)
            {
                listener.OnEventNotify();
            }
        }

        public void RemoveListener(IEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}