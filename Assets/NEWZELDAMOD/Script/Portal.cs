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
        // Removida a parte de salvar respawn

        // Salvar itens da bolsa (se tiver o script Bolsa)
        if (player != null && player.TryGetComponent<Bolsa>(out Bolsa bolsa))
        {
            bolsa.SalvarBolsaNoGameManager();
        }

        // Trocar de cena
        SceneManager.LoadScene(sceneToLoad);
    }
}
