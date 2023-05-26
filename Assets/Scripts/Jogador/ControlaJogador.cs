using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{

    public float Velocidade;

    public int Vida;

    public AudioClip SomDano;

    public LayerMask MascaraChao;

    public LayerMask MascaraParede;

    public GameObject TextoGameOver;

    public ControlaUI ScriptControlaUI;

    private Vector3 direcao;

    private Animator animator;

    private Rigidbody rb;

    private AjustaDirecao ajustaDirecao;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ajustaDirecao = GetComponent<AjustaDirecao>();

        Time.timeScale = 1;
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
        direcao = ajustaDirecao.Ajustar(transform.position, direcao);

        TocarAnimacao();

        if (Vida <= 0)
        {
            Restart();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (direcao * (Velocidade * Time.deltaTime)));

        RotacionarMira();
    }

    public void TomarDano(int dano)
    {
        Vida -= dano;

        ScriptControlaUI.AtualizaSliderVidaJogador();

        ControlaAudio.instancia.PlayOneShot(SomDano);

        if (Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }
    }

    private void RotacionarMira()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100))
        {
            Vector3 posicaoMira = impacto.point - rb.position;
            posicaoMira.y = 0;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMira);

            rb.MoveRotation(novaRotacao);
        }
    }

    private void TocarAnimacao()
    {
        if (direcao != Vector3.zero)
        {
            animator.SetBool("Movendo", true);
        }
        else
        {
            animator.SetBool("Movendo", false);
        }
    }

    private void Restart()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
