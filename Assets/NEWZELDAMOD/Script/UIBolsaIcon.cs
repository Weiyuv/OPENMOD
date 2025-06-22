using UnityEngine;
using UnityEngine.UI;

public class UIBolsaIcon : MonoBehaviour
{
    public GameObject panelInventario;      // Painel do inventário
    public Transform contentItens;          // Content do ScrollView
    public GameObject itemPrefab;           // Prefab do item com Icon + Nome
    public Bolsa bolsa;                     // Referência ao inventário do player

    private bool inventarioAtivo = false;

    void Awake()
    {
        if (bolsa == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                bolsa = player.GetComponent<Bolsa>();
                if (bolsa == null)
                    Debug.LogWarning("Player encontrado, mas sem componente Bolsa!");
            }
            else
            {
                Debug.LogWarning("Nenhum objeto com tag Player encontrado!");
            }
        }
    }

    void Start()
    {
        if (panelInventario != null)
            panelInventario.SetActive(false);
        else
            Debug.LogWarning("panelInventario não atribuído!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventarioAtivo = !inventarioAtivo;

            if (panelInventario != null)
                panelInventario.SetActive(inventarioAtivo);
            else
                Debug.LogWarning("panelInventario não atribuído!");

            if (inventarioAtivo)
                AtualizarUI();
        }
    }

    public void AtualizarUI()
    {
        if (bolsa == null)
        {
            Debug.LogError("Bolsa está NULA. Não dá pra atualizar o inventário!");
            return;
        }

        if (contentItens == null)
        {
            Debug.LogError("contentItens não está atribuído no UIBolsaIcon!");
            return;
        }

        if (itemPrefab == null)
        {
            Debug.LogError("itemPrefab não está atribuído!");
            return;
        }

        Debug.Log("Atualizando UI. Itens na bolsa: " + bolsa.itens.Count);

        // Limpa os filhos antigos
        foreach (Transform child in contentItens)
        {
            Destroy(child.gameObject);
        }

        // Instancia o item para cada elemento da bolsa
        foreach (var item in bolsa.itens)
        {
            Debug.Log("Mostrando item: " + item.nome);

            GameObject obj = Instantiate(itemPrefab, contentItens);

            Transform iconTrans = obj.transform.Find("Icon");
            Transform nomeTrans = obj.transform.Find("Nome");

            if (iconTrans == null || nomeTrans == null)
            {
                Debug.LogError("Prefab do item está faltando os filhos 'Icon' ou 'Nome'!");
                continue;
            }

            Image img = iconTrans.GetComponent<Image>();
            Text txt = nomeTrans.GetComponent<Text>();

            if (img == null || txt == null)
            {
                Debug.LogError("Filho 'Icon' precisa ter Image e 'Nome' precisa ter Text!");
                continue;
            }

            img.sprite = item.icone;
            txt.text = item.nome;
        }
    }

    public void FecharInventario()
    {
        inventarioAtivo = false;
        if (panelInventario != null)
            panelInventario.SetActive(false);
    }
}
