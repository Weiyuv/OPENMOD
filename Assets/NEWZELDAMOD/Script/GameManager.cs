using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 playerPosition;
    public Vector3 respawnPosition;

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

    public void DefinirRespawn(Vector3 novaPosicao)
    {
        respawnPosition = novaPosicao;
        Debug.Log("Novo respawn definido: " + respawnPosition);
    }
}
