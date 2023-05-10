using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
    private Animator animator;

    private Rigidbody physics;

    private Vector3 direcao;

    public float velocidade;

    void Start()
    {
        animator = GetComponent<Animator>();
        physics = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
        {
            animator.SetBool("Movendo", true);
        }
        else
        {
            animator.SetBool("Movendo", false);
        }
    }

    void FixedUpdate()
    {
        physics.MovePosition(physics.position + (direcao * (velocidade * Time.deltaTime)));
    }
}
