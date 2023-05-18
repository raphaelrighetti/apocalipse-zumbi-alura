using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{

    public float Velocidade;

    public GameObject Jogador;

    private Rigidbody physics;

    private Animator animator;



    void Start()
    {
        physics = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 direcao = Jogador.transform.position - physics.position;
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        float distancia = Vector3.Distance(physics.position, Jogador.transform.position);

        physics.MoveRotation(novaRotacao);

        if (distancia > 2.5)
        {
            physics.MovePosition(physics.position + (direcao.normalized * (Velocidade * Time.deltaTime)));

            animator.SetBool("Atacando", false);
        }
        else
        {
            animator.SetBool("Atacando", true);
        }
    }

    void AtacaJogador()
    {
        Jogador.GetComponent<ControlaJogador>().GameOver();
    }
}
