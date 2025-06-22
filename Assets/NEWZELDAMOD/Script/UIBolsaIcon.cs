using UnityEngine;
using UnityEngine.UI;

public class UIBolsaIcon : MonoBehaviour
{
    public GameObject panelInventario;      // Painel do inventário
    public Transform contentItens;          // Conteúdo do ScrollView
    public GameObject itemPrefab;           // Prefab do item com Icon + Nome

    public Bolsa bolsa;                     // Referência ao inventário do player

    private bool inventarioAtivo = false;

    void Start()
    {
        panelInventario.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventarioAtivo = !inventarioAtivo;
            panelInventario.SetActive(inventarioAtivo);

            if (inventarioAtivo)
                AtualizarUI();
        }
    }

    public void AtualizarUI()
    {
        // Limpa os filhos do content
        foreach (Transform child in contentItens)
        {
            Destroy(child.gameObject);
        }

        // Cria um botão para cada item na bolsa
        foreach (var item in bolsa.itens)
        {
            GameObject obj = Instantiate(itemPrefab, contentItens);
            Image img = obj.transform.Find("Icon").GetComponent<Image>();
            Text txt = obj.transform.Find("Nome").GetComponent<Text>();

            img.sprite = item.icone;
            txt.text = item.nome;
        }
    }

    public void FecharInventario()
    {
        inventarioAtivo = false;
        panelInventario.SetActive(false);
    }
}
