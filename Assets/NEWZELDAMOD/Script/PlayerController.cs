using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Bolsa bolsa;
    public List<ItemSO> listaMestraItens;

    void Start()
    {
        if (GameManager.instance != null)
        {
            string cenaAtual = SceneManager.GetActiveScene().name;
            Vector3 posicaoFinal = GameManager.instance.GetPosicaoDaCena(cenaAtual, GameManager.instance.respawnPosition);

            Debug.Log($"[PlayerController] Start - Cena: {cenaAtual}, Posicao recuperada: {posicaoFinal}");

            transform.position = posicaoFinal;

            // Corrige se algum outro script mover o player logo após o Start
            StartCoroutine(ConfirmarPosicaoDepoisDeUmFrame(posicaoFinal));

            if (bolsa != null)
            {
                bolsa.CarregarBolsaDoGameManager(listaMestraItens);
            }
        }
        else
        {
            Debug.LogWarning("[PlayerController] GameManager não encontrado!");
        }
    }

    IEnumerator ConfirmarPosicaoDepoisDeUmFrame(Vector3 pos)
    {
        yield return null;  // Espera 1 frame
        transform.position = pos;
        Debug.Log($"[PlayerController] Posicao forçada após 1 frame: {pos}");
    }

    void Update()
    {
        // Teste: Adicionar item na bolsa com P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (bolsa != null && listaMestraItens.Count > 0)
            {
                bolsa.AdicionarItem(listaMestraItens[0]);
                Debug.Log("[PlayerController] Item adicionado via tecla P.");
            }
        }

        // Teste: Mostrar itens da bolsa com O
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (bolsa != null)
            {
                bolsa.MostrarBolsa();
            }
        }

        // Teste: Simular morte com K
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("[PlayerController] Player morreu. Fazendo respawn...");
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
