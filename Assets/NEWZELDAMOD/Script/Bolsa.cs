using System.Collections.Generic;
using UnityEngine;

public class Bolsa : MonoBehaviour
{
    public List<ItemDisplay> itens = new List<ItemDisplay>();

    public void AdicionarItem(string nomeItem, Sprite iconeItem)
    {
        itens.Add(new ItemDisplay(nomeItem, iconeItem));
        Debug.Log("Item adicionado à bolsa: " + nomeItem);
    }

    public void RemoverItem(string nomeItem)
    {
        ItemDisplay itemRemover = itens.Find(item => item.nome == nomeItem);
        if (itemRemover != null)
        {
            itens.Remove(itemRemover);
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
        foreach (ItemDisplay item in itens)
        {
            Debug.Log(item.nome);
        }
    }

    public void SalvarBolsaNoGameManager()
    {
        GameManager.instance.savedBolsa = new List<string>();
        foreach (ItemDisplay item in itens)
        {
            GameManager.instance.savedBolsa.Add(item.nome);
        }
    }

    public void CarregarBolsaDoGameManager()
    {
        // Aqui você ainda vai precisar de um banco de itens pra recuperar os ícones
    }
}
