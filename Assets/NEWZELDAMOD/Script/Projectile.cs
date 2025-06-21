using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destrói após 3 segundos para limpar cena
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Coloque lógica para dano aqui, ou notifique o inimigo
        Destroy(gameObject); // destrói o projétil ao colidir
    }
}
