using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = (T)this;
    }
}
