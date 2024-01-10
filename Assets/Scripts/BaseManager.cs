using UnityEngine;

public abstract class BaseManager : MonoBehaviour
{
    protected static bool CheckSingletonInstance<T>(T instance, ref T staticInstance) where T : BaseManager
    {
        if (staticInstance == null)
        {
            staticInstance = instance;
            DontDestroyOnLoad(instance.gameObject);
            return true;
        }
        else if (staticInstance != instance)
        {
            Destroy(instance.gameObject);
        }
        
        return false;
    }
}