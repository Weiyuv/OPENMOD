using UnityEngine;
using UnityEngine.UI;

public class UIBolsa : MonoBehaviour
{
    public GameObject panelInventario;      // Painel principal do inventário
    public Transform contentItens;          // Conteúdo do ScrollView onde vão os botões dos itens
    public GameObject itemButtonPrefab;     // Prefab de botão para um item na lista

    public Bolsa bolsa;                     // Referência à bolsa do player

    private bool inventarioAtivo = false;

    void Start()
    {
        panelInventario.SetActive(false); // Começa fechado
    }

    void Update()
    {
        // Abre/fecha inventário com tecla B
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventarioAtivo = !inventarioAtivo;
            panelInventario.SetActive(inventarioAtivo);

            if (inventarioAtivo)
                AtualizarUI();
        }
    }

    // Atualiza os botões da UI conforme os itens da bolsa
    public void AtualizarUI()
    {
        // Limpa itens antigos
        foreach (Transform child in contentItens)
        {
            Destroy(child.gameObject);
        }

        // Cria um botão para cada item da bolsa
        foreach (string item in bolsa.itens)
        {
            GameObject btnObj = Instantiate(itemButtonPrefab, contentItens);
            btnObj.GetComponentInChildren<Text>().text = item;

            // Você pode adicionar eventos aqui (usar item, remover, etc)
        }
    }

    // Botão FECHAR pode chamar essa função
    public void FecharInventario()
    {
        inventarioAtivo = false;
        panelInventario.SetActive(false);
    }
}
