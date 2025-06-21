using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    public Rigidbody rb;                  // Rigidbody do personagem
    public float jumpForce = 15f;          // Força do super pulo
    public GameObject jumpEffectPrefab;    // (Opcional) Efeito visual
    private bool isGrounded = true;        // Simples controle de chão

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            PerformSuperJump();
        }
    }

    void PerformSuperJump()
    {
        // Reseta o Y antes de pular
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        // Instancia efeito visual
        if (jumpEffectPrefab != null)
        {
            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
        }

        isGrounded = false;  // Bloqueia até tocar o chão de novo
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Considera no chão ao tocar qualquer coisa (melhore se quiser)
        isGrounded = true;
    }
}
