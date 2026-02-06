using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, object> listeners = new Dictionary<Type, object>();

    public static void Subscribe<T>(Action<T> callback)
    {
        Type type = typeof(T);

        if (listeners.ContainsKey(type))
        {
            listeners[type] = (Action<T>)listeners[type] + callback;
        }
        else
        {
            listeners.Add(type, callback);
        }
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        Type type = typeof(T);

        if (!listeners.ContainsKey(type))
        {
            return;
        }

        Action<T> current = (Action<T>)listeners[type];
        current -= callback;

        if (current == null)
        {
            listeners.Remove(type);
        }
        else
        {
            listeners[type] = current;
        }
    }

    public static void Publish<T>(T message)
    {
        Type type = typeof(T);

        if (!listeners.ContainsKey(type))
        {
            return;
        }

        Action<T> callback = (Action<T>)listeners[type];
        callback(message);
    }
}
