using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    protected bool _isPersistent; // Whether or not this instance should call DoNotDestroyOnLoad

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T; // Declare an instance of the required type

            if(_isPersistent == true)
            {
                DontDestroyOnLoad(Instance);
            }
        }
        else if(_instance != null)
        {
            Destroy(this.gameObject); // Destroy any duplicates of the singleton
        }
    }

    protected virtual void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null; // Avoid any supernatural, ghostly references to the singleton when this object is destroyed
        }
    }

}
