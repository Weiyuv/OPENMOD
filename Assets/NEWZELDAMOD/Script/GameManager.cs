using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 playerPosition;
    public Vector3 respawnPosition;

    public List<ItemSO> itensDaBolsa = new List<ItemSO>();

    [Header("Posições salvas por cena")]
    public List<ScenePositionData> scenePositions = new List<ScenePositionData>();

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
    }

    public void DefinirRespawn(Vector3 novaPosicao)
    {
        respawnPosition = novaPosicao;
        Debug.Log("Novo respawn definido: " + respawnPosition);
    }

    public void SalvarPosicaoCena(string sceneName, Vector3 pos)
    {
        bool achou = false;
        for (int i = 0; i < scenePositions.Count; i++)
        {
            if (scenePositions[i].sceneName == sceneName)
            {
                scenePositions[i].position = pos;
                achou = true;
                break;
            }
        }

        if (!achou)
        {
            scenePositions.Add(new ScenePositionData { sceneName = sceneName, position = pos });
        }

        Debug.Log("Posição salva para a cena: " + sceneName + " -> " + pos);
    }

    public Vector3 GetPosicaoDaCena(string sceneName, Vector3 posicaoPadrao)
    {
        foreach (var data in scenePositions)
        {
            if (data.sceneName == sceneName)
            {
                Debug.Log("Posição carregada para a cena: " + sceneName + " -> " + data.position);
                return data.position;
            }
        }

        Debug.Log("Nenhuma posição salva pra cena: " + sceneName + ". Usando posição padrão.");
        return posicaoPadrao;
    }
}
