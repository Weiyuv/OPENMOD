using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    private string lastScene;
    private Vector3 lastPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveTeleportData(string sceneName, Vector3 position)
    {
        lastScene = sceneName;
        lastPosition = position;
    }

    public string GetLastScene()
    {
        return lastScene;
    }

    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }
}
