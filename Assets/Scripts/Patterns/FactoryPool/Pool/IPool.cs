using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T> : IPool where T : IPooledObject
{
    T Pull();
}

public interface IPool
{
    void Push(IPooledObject pooledObject);
}
