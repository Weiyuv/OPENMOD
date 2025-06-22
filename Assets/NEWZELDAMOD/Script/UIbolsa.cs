using UnityEngine;
using UnityEngine.UI;

public class UIBolsa : MonoBehaviour
{
    public GameObject panelInventario;
    public Transform contentItens;
    public GameObject itemButtonPrefab;
    public Bolsa bolsa;

    private bool inventarioAtivo = false;

    void Start()
    {
        panelInventario.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventarioAtivo = !inventarioAtivo;
            panelInventario.SetActive(inventarioAtivo);

            if (inventarioAtivo)
                AtualizarUI();
        }
    }

    public void AtualizarUI()
    {
        foreach (Transform child in contentItens)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemDisplay item in bolsa.itens)
        {
            GameObject btnObj = Instantiate(itemButtonPrefab, contentItens);

            Text texto = btnObj.GetComponentInChildren<Text>();
            Image imagem = btnObj.transform.Find("Icone").GetComponent<Image>();

            if (texto != null)
                texto.text = item.nome;

            if (imagem != null && item.icone != null)
                imagem.sprite = item.icone;
        }
    }

    public void FecharInventario()
    {
        inventarioAtivo = false;
        panelInventario.SetActive(false);
    }
}
