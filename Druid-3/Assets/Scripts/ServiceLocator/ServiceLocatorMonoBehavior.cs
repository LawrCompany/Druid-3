using System.Collections.Generic;
using UnityEngine;


// ReSharper disable once CheckNamespace
public static class ServiceLocatorMonoBehavior
{
    #region Properties

    #endregion


    #region Fields

    private static Dictionary<object, object> _serviceContainer = null;

    #endregion


    #region Methods

    public static T GetService<T>(bool createObjectIfNotFound = true) where T : UnityEngine.Object
    {
        if (_serviceContainer == null)
        {
            _serviceContainer = new Dictionary<object, object>();
        }

        if (!_serviceContainer.ContainsKey(typeof(T)))
        {
            return FindService<T>(createObjectIfNotFound);
        }

        var service = (T) _serviceContainer[typeof(T)];
        if (service != null)
        {
            return service;
        }

        _serviceContainer.Remove(typeof(T));
        return FindService<T>(createObjectIfNotFound);
    }

    private static T FindService<T>(bool createObjectIfNotFound) where T : Object
    {
        T type = Object.FindObjectOfType<T>();
        if (type != null)
        {
            _serviceContainer.Add(typeof(T), type);
        }
        else if (createObjectIfNotFound)
        {
            var go = new GameObject(typeof(T).Name, typeof(T));
            _serviceContainer.Add(typeof(T), go.GetComponent<T>());
        }

        return (T) _serviceContainer[typeof(T)];
    }

    #endregion
}