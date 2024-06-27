using UnityEngine;

public class MonoPooled : MonoBehaviour, IPooledObject
{
    private IPool _pool;//Parent pool

    public virtual void Initialize()//Enable object after storage
    {
        gameObject.SetActive(true);
    }

    public virtual void ReturnToPool()//Disable object and return it to storage
    {
        gameObject.SetActive(false);
        _pool.Push(this);
    }

    public void SetParentPool<T>(IPool<T> parent) where T : IPooledObject//Set parent
    {
        _pool = parent;
    }
}