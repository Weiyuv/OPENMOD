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

    public void RemoverItem(ItemSO item)
    {
        if (itens.Contains(item))
        {
            itens.Remove(item);
            Debug.Log("Item removido da bolsa: " + item.nome);
        }
        else
        {
            Debug.Log("Item não encontrado na bolsa: " + item.nome);
        }
    }

    public void MostrarBolsa()
    {
        Debug.Log("Conteúdo da bolsa:");
        foreach (ItemSO item in itens)
        {
            Debug.Log(item.nome);
        }
    }

    public void SalvarBolsaNoGameManager()
    {
        List<string> nomes = new List<string>();
        foreach (ItemSO item in itens)
            nomes.Add(item.name);

        GameManager.instance.savedBolsaNomes = nomes;
    }

    public void CarregarBolsaDoGameManager(List<ItemSO> listaMestra)
    {
        itens.Clear();
        foreach (string nome in GameManager.instance.savedBolsaNomes)
        {
            ItemSO item = listaMestra.Find(i => i.name == nome);
            if (item != null)
                itens.Add(item);
            else
                Debug.LogWarning($"Item {nome} não encontrado na lista mestra!");
        }
    }
}
