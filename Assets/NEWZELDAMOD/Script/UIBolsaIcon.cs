using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBolsaIcon : MonoBehaviour
{
    public GameObject panelInventario;
    public Transform contentItens;
    public GameObject itemPrefab;
    public Bolsa bolsa;

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
        if (panelInventario != null)
            panelInventario.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventarioAtivo = !inventarioAtivo;
            if (panelInventario != null)
                panelInventario.SetActive(inventarioAtivo);

            if (inventarioAtivo)
                AtualizarUI();
        }
    }

    public void AtualizarUI()
    {
        if (bolsa == null)
        {
            Debug.LogError("Bolsa não encontrada!");
            return;
        }

        foreach (Transform child in contentItens)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in bolsa.itens)
        {
            Debug.Log("Mostrando item: " + item.nome);

            GameObject obj = Instantiate(itemPrefab, contentItens);

            Transform iconTrans = obj.transform.Find("Icon");
            if (iconTrans != null)
            {
                Image img = iconTrans.GetComponent<Image>();
                if (img != null)
                    img.sprite = item.icone;
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(contentItens.GetComponent<RectTransform>());
    }

    public void FecharInventario()
    {
        inventarioAtivo = false;
        if (panelInventario != null)
            panelInventario.SetActive(false);
    }
}
