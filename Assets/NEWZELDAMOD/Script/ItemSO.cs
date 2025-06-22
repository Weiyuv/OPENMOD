using UnityEngine;

[CreateAssetMenu(fileName = "NovoItem", menuName = "Inventario/Item")]
public class ItemSO : ScriptableObject
{
    public string nome;
    public Sprite icone;
}
