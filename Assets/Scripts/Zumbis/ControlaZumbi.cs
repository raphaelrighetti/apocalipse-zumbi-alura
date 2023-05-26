using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{

    public float Velocidade;

    public int DanoMinimo;

    public int DanoMaximo;

    public AudioClip SomMorte;

    private GameObject jogador;

    private Rigidbody rb;

    private Animator animator;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        jogador = GameObject.FindWithTag("Jogador");

        EscolherSkin();
    }

    void FixedUpdate()
    {
        Vector3 direcao = jogador.transform.position - rb.position;
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        float distancia = Vector3.Distance(rb.position, jogador.transform.position);

        rb.MoveRotation(novaRotacao);

        if (distancia > 2.5)
        {
            rb.MovePosition(rb.position + (direcao.normalized * (Velocidade * Time.deltaTime)));

            animator.SetBool("Atacando", false);
        }
        else
        {
            animator.SetBool("Atacando", true);
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);

        ControlaAudio.instancia.PlayOneShot(SomMorte);
    }

    private void EscolherSkin()
    {
        int zumbiEscolhido = Random.Range(1, 28);

        transform.GetChild(zumbiEscolhido).gameObject.SetActive(true);
    }

    private void AtacarJogador()
    {
        int dano = Random.Range(DanoMinimo, DanoMaximo + 1);

        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }
}
