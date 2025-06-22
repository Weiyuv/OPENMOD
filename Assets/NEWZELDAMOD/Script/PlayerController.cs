using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Bolsa bolsa;
    public List<ItemSO> listaMestraItens;  // Arraste aqui todos os itens criados no editor

    void Start()
    {
        if (GameManager.instance != null)
        {
            transform.position = GameManager.instance.playerPosition;

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
        // Exemplo: adicionar o primeiro item da lista mestra ao apertar 'P'
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (bolsa != null && listaMestraItens.Count > 0)
            {
                bolsa.AdicionarItem(listaMestraItens[0]);
            }
        }

        // Mostrar bolsa no console ao apertar 'O'
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (bolsa != null)
            {
                bolsa.MostrarBolsa();
            }
        }
    }
}
