using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel
{
    public AudioClip SomDano;
    public LayerMask MascaraChao;
    public LayerMask MascaraParede;
    public ControlaUI ScriptControlaUI;
    private Vector3 direcao;
    private Animator animator;
    private Rigidbody rb;
    private StatusPersonagem status;
    private MovimentacaoPersonagem movimentacaoJogador;
    private AjustaDirecao ajustaDirecao;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<StatusPersonagem>();
        movimentacaoJogador = GetComponent<MovimentacaoPersonagem>();
        ajustaDirecao = GetComponent<AjustaDirecao>();

        Time.timeScale = 1;
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
        direcao = ajustaDirecao.Ajustar(transform.position, direcao);

        if (direcao != Vector3.zero)
        {
            animator.SetBool("Movendo", true);
        }
        else
        {
            animator.SetBool("Movendo", false);
        }

        if (status.Vida <= 0)
        {
            Restart();
        }
    }

    void FixedUpdate()
    {
        movimentacaoJogador.Movimentar(direcao, status.Velocidade);

        RotacionarMira();
    }

    private void RotacionarMira()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100))
        {
            Vector3 posicaoMira = impacto.point - rb.position;

            movimentacaoJogador.Rotacionar(posicaoMira);
        }
    }

    private void Restart()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void TomarDano(int dano)
    {
        status.Vida -= dano;

        ScriptControlaUI.AtualizaSliderVidaJogador();

        ControlaAudio.instancia.PlayOneShot(SomDano);

        if (status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        ScriptControlaUI.GameOver();
    }
}
