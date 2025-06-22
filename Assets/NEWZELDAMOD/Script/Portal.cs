using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    private bool playerInRange;
    private Transform player;
    private Bolsa bolsa;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            bolsa = player.GetComponent<Bolsa>();
            if (bolsa == null)
                Debug.LogWarning("Bolsa não encontrada no player!");
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
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

    void TeleportPlayer()
    {
        if (player != null)
        {
            GameManager.instance.playerPosition = player.position;

            if (bolsa != null)
            {
                bolsa.SalvarBolsaNoGameManager();
            }
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
