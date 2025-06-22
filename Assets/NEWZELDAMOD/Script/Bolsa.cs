using System.Collections.Generic;
using UnityEngine;

public class Bolsa : MonoBehaviour
{
    public List<ItemSO> itens = new List<ItemSO>();

    public void AdicionarItem(ItemSO item)
    {
        itens.Add(item);
        Debug.Log("Item adicionado à bolsa: " + item.nome);
    }

    public void MostrarBolsa()
    {
        foreach (var item in itens)
        {
            Debug.Log("Item na bolsa: " + item.nome);
        }
    }

    public void SalvarBolsaNoGameManager()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SalvarBolsa(itens);
        }
    }

    public void CarregarBolsaDoGameManager(List<ItemSO> listaMestra)
    {
        itens.Clear();

        if (GameManager.instance != null && GameManager.instance.itensDaBolsa.Count > 0)
        {
            foreach (ItemSO item in GameManager.instance.itensDaBolsa)
            {
                if (listaMestra.Contains(item))
                    itens.Add(item);
            }
            Debug.Log("Bolsa carregada com " + itens.Count + " itens do GameManager.");
        }
        else
        {
            Debug.Log("Nenhum item salvo no GameManager para carregar.");
        }
    }
}
