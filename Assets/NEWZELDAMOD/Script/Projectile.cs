using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destr�i ap�s 3 segundos para limpar cena
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Coloque l�gica para dano aqui, ou notifique o inimigo
        Destroy(gameObject); // destr�i o proj�til ao colidir
    }
}
