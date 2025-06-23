using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    private bool playerInRange;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
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
        if (GameManager.instance != null && player != null)
        {
            string cenaAtual = SceneManager.GetActiveScene().name;
            GameManager.instance.SalvarPosicaoCena(cenaAtual, player.position);

            if (player.TryGetComponent<Bolsa>(out Bolsa bolsa))
            {
                bolsa.SalvarBolsaNoGameManager();
            }
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
