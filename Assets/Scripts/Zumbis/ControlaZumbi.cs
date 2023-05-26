using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{

    public float Velocidade;

    public int DanoMinimo;

    public int DanoMaximo;

    private GameObject jogador;

    private Rigidbody physics;

    private Animator animator;



    void Start()
    {
        physics = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        jogador = GameObject.FindWithTag("Jogador");

        int zumbiEscolhido = Random.Range(1, 28);

        transform.GetChild(zumbiEscolhido).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        Vector3 direcao = jogador.transform.position - physics.position;
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        float distancia = Vector3.Distance(physics.position, jogador.transform.position);

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
        int dano = Random.Range(DanoMinimo, DanoMaximo + 1);

        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }
}
