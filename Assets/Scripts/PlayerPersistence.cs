using UnityEngine;

public class PlayerPersistence : MonoBehaviour
{
    public static PlayerPersistence instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}