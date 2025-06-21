using UnityEngine;

public class SimpleInventory : MonoBehaviour
{
    public GameObject inventoryUI;  // Arraste aqui o painel de UI do inventário (Canvas > Panel)

    private bool isOpen = false;

    void Start()
    {
        // Começa fechado
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;

        if (inventoryUI != null)
        {
            inventoryUI.SetActive(isOpen);
        }

        // (Opcional) Pausar o jogo enquanto o inventário está aberto
        if (isOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
