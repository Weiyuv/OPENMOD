using UnityEngine;

[System.Serializable]
public class ItemDisplay
{
    public string nome;
    public Sprite icone;

    public ItemDisplay(string nome, Sprite icone)
    {
        this.nome = nome;
        this.icone = icone;
    }
}
