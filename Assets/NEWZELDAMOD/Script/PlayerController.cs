using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Bolsa bolsa;
    public List<ItemSO> listaMestraItens;

    void Start()
    {
        if (GameManager.instance != null)
        {
            // Faz o player nascer na posição de respawn
            transform.position = GameManager.instance.respawnPosition;

            // Carrega a bolsa
            if (bolsa != null)
            {
                bolsa.CarregarBolsaDoGameManager(listaMestraItens);
            }
            else
            {
                Debug.LogWarning("Bolsa não atribuída no PlayerController!");
            }
        }
        else
        {
            Debug.LogWarning("GameManager não encontrado na cena!");
        }
    }

    void Update()
    {
        // Exemplo: adicionar o primeiro item da lista ao apertar 'P'
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (bolsa != null && listaMestraItens.Count > 0)
            {
                bolsa.AdicionarItem(listaMestraItens[0]);
            }
        }

        // Mostrar a bolsa no console com 'O'
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (bolsa != null)
            {
                bolsa.MostrarBolsa();
            }
        }

        // TESTE DE MORTE E RESPAWN: apertar K para "morrer"
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Player morreu. Fazendo respawn...");
            MorrerERespawnar();
        }
    }

    public void MorrerERespawnar()
    {
        if (GameManager.instance != null && bolsa != null)
        {
            bolsa.SalvarBolsaNoGameManager();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
