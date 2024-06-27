using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : IPool<T> where T : IPooledObject
{
    private readonly Transform _parentTransform; //Parent
    private readonly IFactory<T> _factory; //Factory
    private readonly List<T> _pooledObjects = new List<T>(); //Objects in storage

    public Pool(IFactory<T> factory, Transform parent) //Construct
    {
        _parentTransform = parent; //Pool parent
        _factory = factory; //Factory
    }

    public T Pull() //Returns an item from the storage, if the storage is empty, it creates a new one
    {
        if (_pooledObjects.Count == 0) //Checking for item availability in storage
        {
            _pooledObjects.Add(NewPoolObject()); //Create new item, and save to storage
        }

        var returnValue = _pooledObjects[0]; //Take first item on storage
        returnValue.Initialize(); //Initialize item
        _pooledObjects.Remove(returnValue); //Remove an object from storage
        return returnValue;
    }

    public void Push(IPooledObject pooledObject) //Returns the object back to the storage
    {
        _pooledObjects.Add((T)pooledObject);
    }

    private T NewPoolObject() //creates a new object from the factory
    {
        var returnValue = _factory.CreatePoolObject();
        returnValue.SetParentPool(this);
        return returnValue;
    }
}