using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel
{
    public float RaioRoaming;
    public float ContadorVagarMax;
    public float DistanciaMaximaObjeto;
    public AudioClip SomMorte;
    public LayerMask MascaraZumbi;
    private int danoMinimo;
    private int danoMaximo;
    private float contadorVagar;
    private bool inicializado;
    private Vector3 direcao;
    private Vector3 posicaoAleatoria;
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

        contadorVagar = ContadorVagarMax;

        EscolherSkin();
    }

    void FixedUpdate()
    {
        if (!inicializado)
        {
            return;
        }

        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            PerseguirJogador();
        }
        else
        {
            direcao = (jogador.transform.position - rb.position).normalized;
            animator.SetBool("Atacando", true);
        }

        movimentacaoZumbi.Rotacionar(direcao);
        animator.SetFloat("Movimentando", direcao.magnitude);
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

    private void Init()
    {
        inicializado = true;

        animator.SetBool("Inicializado", true);
    }

    private void PerseguirJogador()
    {
        direcao = (jogador.transform.position - rb.position).normalized;
        movimentacaoZumbi.Movimentar(direcao, status.Velocidade);

        animator.SetBool("Atacando", false);
    }

    private void Vagar()
    {
        if (contadorVagar >= ContadorVagarMax)
        {
            posicaoAleatoria = AleatorizarPosicao();
            direcao = (posicaoAleatoria - transform.position).normalized;

            contadorVagar = 0;
        }

        bool pertoDaPosicao = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (!pertoDaPosicao)
        {
            Vector3 posicaoRaycast = transform.position;
            posicaoRaycast.y = 1;

            if (Physics.Raycast(posicaoRaycast, direcao, DistanciaMaximaObjeto, ~MascaraZumbi))
            {
                direcao = -direcao;
                posicaoAleatoria = transform.position + (direcao * RaioRoaming);
            }

            movimentacaoZumbi.Movimentar(direcao, status.Velocidade);
        }
        else
        {
            direcao = Vector3.zero;
        }

        contadorVagar += Time.deltaTime;
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * RaioRoaming;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
}
