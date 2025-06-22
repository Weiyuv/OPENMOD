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
            }
        }
    }

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
        foreach (Transform child in contentItens)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in bolsa.itens)
        {
            GameObject obj = Instantiate(itemPrefab, contentItens);

            Transform iconTrans = obj.transform.Find("Icon");
            Transform nomeTrans = obj.transform.Find("Nome");

            if (iconTrans != null && nomeTrans != null)
            {
                Image img = iconTrans.GetComponent<Image>();
                Text txt = nomeTrans.GetComponent<Text>();

                if (img != null)
                    img.sprite = item.icone;

                if (txt != null)
                    txt.text = item.nome;
            }
        }
    }

    public void FecharInventario()
    {
        inventarioAtivo = false;
        panelInventario.SetActive(false);
    }
}
