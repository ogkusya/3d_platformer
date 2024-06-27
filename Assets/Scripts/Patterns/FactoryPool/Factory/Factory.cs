using UnityEngine;

public class Factory<T> : IFactory<T>
{
    private readonly GameObject _prefab;

    public Factory(GameObject prefab) //Factory constructor
    {
        _prefab = prefab;
    }

    public T CreatePoolObject() //Create new object
    {
        var newObject = GameObject.Instantiate(_prefab);

        var returnValue = newObject.GetComponent<T>();
        return returnValue;
    }
}
