using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel
{


    public AudioClip SomMorte;

    private int danoMinimo;

    private int danoMaximo;

    private GameObject jogador;

    private Rigidbody rb;

    private Animator animator;

    private StatusPersonagem status;

    private MovimentacaoPersonagem movimentacaoZumbi;



    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<StatusPersonagem>();
        movimentacaoZumbi = GetComponent<MovimentacaoPersonagem>();

        danoMinimo = status.Dano / 2;
        danoMaximo = status.Dano;

        EscolherSkin();
    }

    void FixedUpdate()
    {
        Vector3 direcao = (jogador.transform.position - rb.position).normalized;
        float distancia = Vector3.Distance(rb.position, jogador.transform.position);

        movimentacaoZumbi.Rotacionar(direcao);

        if (distancia > 2.5)
        {
            movimentacaoZumbi.Movimentar(direcao, status.Velocidade);

            animator.SetBool("Atacando", false);
        }
        else
        {
            animator.SetBool("Atacando", true);
        }
    }

    public void TomarDano(int dano)
    {
        status.Vida -= dano;

        ControlaAudio.instancia.PlayOneShot(SomMorte);

        if (status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
    }

    private void EscolherSkin()
    {
        int zumbiEscolhido = Random.Range(1, 28);

        transform.GetChild(zumbiEscolhido).gameObject.SetActive(true);
    }

    private void AtacarJogador()
    {
        int dano = Random.Range(danoMinimo, danoMaximo + 1);

        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }
}
