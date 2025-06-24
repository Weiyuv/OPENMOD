using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public List<ItemSO> itensDaBolsa = new List<ItemSO>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SalvarBolsa(List<ItemSO> itensAtuais)
    {
        itensDaBolsa = new List<ItemSO>(itensAtuais);
        Debug.Log("Itens da bolsa salvos: " + itensDaBolsa.Count);
    }

    // Método DefinirRespawn removido
}
