using System.Collections.Generic;
using UnityEngine;

public class Bolsa : MonoBehaviour
{
    public List<string> itens = new List<string>();

    public void AdicionarItem(string nomeItem)
    {
        itens.Add(nomeItem);
        Debug.Log("Item adicionado à bolsa: " + nomeItem);
    }

    public void RemoverItem(string nomeItem)
    {
        if (itens.Contains(nomeItem))
        {
            itens.Remove(nomeItem);
            Debug.Log("Item removido da bolsa: " + nomeItem);
        }
        else
        {
            Debug.Log("Item não encontrado na bolsa: " + nomeItem);
        }
    }

    public void MostrarBolsa()
    {
        Debug.Log("Conteúdo da bolsa:");
        foreach (string item in itens)
        {
            Debug.Log(item);
        }
    }

    public void SalvarBolsaNoGameManager()
    {
        GameManager.instance.savedBolsa = new List<string>(itens);
    }

    public void CarregarBolsaDoGameManager()
    {
        itens = new List<string>(GameManager.instance.savedBolsa);
    }
}
