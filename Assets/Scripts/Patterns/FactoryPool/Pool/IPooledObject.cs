public interface IPooledObject
{
    void ReturnToPool(); //Return object to storage
    void Initialize(); //Called when an object is retrieved from storage
    void SetParentPool<T>(IPool<T> parent) where T : IPooledObject; //Set storage
}
