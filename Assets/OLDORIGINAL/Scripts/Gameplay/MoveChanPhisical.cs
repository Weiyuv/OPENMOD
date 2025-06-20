﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveChanPhisical : MonoBehaviour
{
    public int AlturaAgua = 31;

    public Rigidbody rdb;
    public Animator anim;
    Vector3 movaxis;
    public GameObject currentCamera;
    public float jumpspeed = 8;
    public float gravity = 20;

    public bool haveWeapons = false;  // <-- Adicionado aqui ✅

    float jumptime;
    float flyvelocity = 3;
    public GameObject wing;
    public Transform rightHandObj, leftHandObj;
    bool jumpbtn = false;
    bool grounded = false;
    bool jumpbtndown = false;
    GameObject closeThing;
    float weight;
    FixedJoint joint;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Land"))
        {
            if (PlayerPrefs.HasKey("OldPlayerPosition"))
            {
                print("movendo " + PlayerPrefsX.GetVector3("OldPlayerPosition"));
                transform.position = PlayerPrefsX.GetVector3("OldPlayerPosition");
            }
        }
        currentCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpbtn = true;
            jumpbtndown = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpbtn = false;
            jumptime = 0;
        }
    }

    void FixedUpdate()
    {
        movaxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        anim.SetFloat("Speed", rdb.linearVelocity.magnitude);

        if (wing.activeSelf)
        {
            FlyControl();
        }
        else
        {
            GroundControl();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("PunchA");
            Hit(); // Chama o ataque quando apertar o botão
        }

        if (Input.GetButton("Fire1") && wing.activeSelf)
        {
            rdb.AddRelativeForce(Vector3.forward * 5000);
        }

        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position - (transform.forward * 0.1f) + transform.up * 0.3f, Vector3.down, out hit, 1000))
        {
            anim.SetFloat("JumpHeight", hit.distance);

            if (hit.distance < 0.5f)
            {
                grounded = true;
            }

            if (grounded && jumpbtn)
            {
                jumptime = 0.25f;
            }

            if (!grounded && jumpbtndown && !wing.activeSelf)
            {
                wing.SetActive(true);
                jumpbtndown = false;
                return;
            }
            if (!grounded && jumpbtndown && wing.activeSelf)
            {
                wing.SetActive(false);
            }
        }

        if (jumpbtn)
        {
            jumptime -= Time.fixedDeltaTime;
            jumptime = Mathf.Clamp01(jumptime);
            rdb.AddForce(jumpspeed * jumptime * Vector3.up);
        }

        jumpbtndown = false;
    }

    private void GroundControl()
    {
        Vector3 relativedirection = currentCamera.transform.TransformVector(movaxis).normalized;
        relativedirection = new Vector3(relativedirection.x, jumptime, relativedirection.z);
        Vector3 relativeDirectionWOy = new Vector3(relativedirection.x, 0, relativedirection.z);
        if (grounded)
        {
            rdb.linearVelocity = new Vector3(relativedirection.x * 5, rdb.linearVelocity.y, relativedirection.z * 5);
        }
        else
        {
            rdb.AddForce(new Vector3(relativedirection.x * 500, 0, relativedirection.z * 500));
        }

        if (!joint)
        {
            Quaternion rottogo = Quaternion.LookRotation(relativeDirectionWOy * 2 + transform.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, rottogo, Time.fixedDeltaTime * 50);
        }

        if (transform.position.y < AlturaAgua)
        {
            rdb.AddForce(Vector3.up * 1200);
            rdb.linearDamping = 4;
        }
        else
        {
            rdb.linearDamping = 1;
        }
    }

    void OnAnimatorIK()
    {
        if (wing.activeSelf && rightHandObj != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
        }

        if (closeThing)
        {
            Vector3 handDirection = closeThing.transform.position - transform.position;
            float lookto = Vector3.Dot(handDirection.normalized, transform.forward);
            weight = Mathf.Lerp(weight, (lookto * 3 / (Mathf.Pow(handDirection.magnitude, 3))), Time.fixedDeltaTime * 2);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, closeThing.transform.position + transform.right * 0.1f);
            anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.identity);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, closeThing.transform.position - transform.right * 0.1f);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.identity);

            if (Input.GetButtonDown("Fire1"))
            {
                // Aqui poderia ter alguma interação com o objeto
            }

            if (weight <= 0)
            {
                Destroy(closeThing);
                if (joint)
                {
                    Destroy(joint);
                    return;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        wing.SetActive(false);

        if (collision.transform.position.y > transform.position.y + .05f)
        {
            if (!closeThing)
                closeThing = new GameObject("Handpos");

            weight = 0;
            closeThing.transform.parent = collision.gameObject.transform;
            closeThing.transform.position = collision.GetContact(0).point;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Nada necessário aqui
    }

    void FlyControl()
    {
        rdb.linearDamping = 0.4f;
        float velocity = Mathf.Abs(rdb.linearVelocity.x) + Mathf.Abs(rdb.linearVelocity.z);
        velocity = Mathf.Clamp(velocity, 0, 10);

        rdb.AddRelativeForce(new Vector3(0, velocity * 50, 500));

        Vector3 movfly = new Vector3(Vector3.forward.x * flyvelocity, 0, Vector3.forward.z * flyvelocity);

        float angz = Vector3.Dot(transform.right, Vector3.up);
        float angx = Vector3.Dot(transform.forward, Vector3.up);
        movfly = new Vector3(movaxis.z + angx * 2, -angz, -movaxis.x - angz);

        transform.Rotate(movfly);

        wing.transform.localRotation = Quaternion.Euler(0, 0, angz * 50);

        flyvelocity -= angx * 0.01f;
        flyvelocity = Mathf.Lerp(flyvelocity, 3, Time.fixedDeltaTime);
        flyvelocity = Mathf.Clamp(flyvelocity, 0, 5);
    }

    // =========================
    // SISTEMA DE DANO AQUI 👇👇
    // =========================

    public void Hit()
    {
        DealDamage();
    }

    void DealDamage()
    {
        float range = 3f;  // Alcance do ataque
        int damage = 20;   // Dano causado
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");

        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up;  // Origem um pouco acima do chão
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * range, Color.red, 1f); // Para visualizar o raio no editor

        if (Physics.Raycast(origin, direction, out hit, range, enemyLayer))
        {
            Debug.Log("Acertou inimigo: " + hit.collider.name);

            Vida vida = hit.collider.GetComponent<Vida>();
            if (vida != null)
            {
                vida.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Inimigo atingido não tem componente Vida!");
            }
        }
        else
        {
            Debug.Log("Ataque não acertou ninguém.");
        }
    }
}
