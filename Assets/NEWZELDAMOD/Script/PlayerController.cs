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
            // Remove posicionar player no respawn (pois não existe mais)
            // transform.position = GameManager.instance.respawnPosition; 

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

        // Mostrar a bolsa no console com 'B'
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (bolsa != null)
            {
                bolsa.MostrarBolsa();
            }
        }

        // TESTE DE MORTE E RECARREGAR CENA: apertar K para "morrer"
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Player morreu. Recarregando cena...");
            MorrerERespawnar();
        }
    }

    public void MorrerERespawnar()
    {
        if (GameManager.instance != null && bolsa != null)
        {
            bolsa.SalvarBolsaNoGameManager();
        }

        // Apenas recarrega a cena atual (sem reposicionar player via respawn)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
