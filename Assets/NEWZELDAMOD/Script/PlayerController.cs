using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Bolsa bolsa;

    void Start()
    {
        if (GameManager.instance != null)
        {
            transform.position = GameManager.instance.playerPosition;

            if (bolsa != null)
            {
                bolsa.CarregarBolsaDoGameManager();
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (bolsa != null)
                bolsa.AdicionarItem("Espada");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (bolsa != null)
                bolsa.MostrarBolsa();
        }
    }
}
