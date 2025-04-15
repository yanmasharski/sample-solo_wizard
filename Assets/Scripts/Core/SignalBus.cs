using System;
using System.Collections.Generic;
using UnityEngine;

public static class SignalBus
{
    private static readonly Dictionary<Type, List<Delegate>> listeners =
        new Dictionary<Type, List<Delegate>>();

    public static void Subscribe<T>(Action<T> callback)
        where T : ISignal
    {
        var type = typeof(T);

        if (listeners.TryGetValue(type, out var list))
        {
            list.Add(callback);
        }
        else
        {
            listeners[type] = new List<Delegate> { callback };
        }
    }

    public static void Unsubscribe<T>(Action<T> callback)
        where T : ISignal
    {
        var type = typeof(T);

        if (!listeners.TryGetValue(type, out var list))
        {
            return;
        }

        list.Remove(callback);
    }

    public static void Fire<T>(T signal)
        where T : ISignal
    {
        var type = typeof(T);

        if (!listeners.TryGetValue(type, out var list))
        {
            return;
        }

        var listenersCopy = new List<Delegate>(list);

        foreach (var listener in listenersCopy)
        {
            try
            {
                var callback = listener as Action<T>;
                callback?.Invoke(signal);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    public static void ClearSignalListeners<T>()
        where T : ISignal
    {
        var type = typeof(T);
        listeners.Remove(type);
    }

    public static void ClearAllListeners()
    {
        listeners.Clear();
    }
}
