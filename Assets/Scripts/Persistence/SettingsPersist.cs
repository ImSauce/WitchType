using UnityEngine;

public class SettingsPersist : MonoBehaviour
{
    public static SettingsPersist instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

