using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalReturn : MonoBehaviour
{
    private bool playerInRange;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            string lastScene = TeleportManager.Instance.GetLastScene();
            if (!string.IsNullOrEmpty(lastScene))
            {
                SceneManager.LoadScene(lastScene);
            }
            else
            {
                Debug.LogWarning("Cena anterior não encontrada!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Vector3 lastPosition = TeleportManager.Instance.GetLastPosition();
            playerObj.transform.position = lastPosition;
            Debug.Log("Jogador teleportado de volta para: " + lastPosition);
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado na cena após o carregamento.");
        }
    }
}
