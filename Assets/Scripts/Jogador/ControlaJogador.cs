using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{

    public float Velocidade;

    public int Vida;

    public LayerMask MascaraChao;

    public LayerMask MascaraParede;

    public GameObject TextoGameOver;

    public ControlaUI ScriptControlaUI;

    private bool encostandoForward;

    private bool encostandoRight;

    private bool encostandoBack;

    private bool encostandoLeft;

    private Animator animator;

    private Rigidbody physics;

    private Vector3 direcao;

    void Start()
    {
        animator = GetComponent<Animator>();
        physics = GetComponent<Rigidbody>();

        Time.timeScale = 1;
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        EncostandoParede();
        AjustaDirecao();

        if (direcao != Vector3.zero)
        {
            animator.SetBool("Movendo", true);
        }
        else
        {
            animator.SetBool("Movendo", false);
        }

        if (Vida <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    void FixedUpdate()
    {
        physics.MovePosition(physics.position + (direcao * (Velocidade * Time.deltaTime)));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100))
        {
            Vector3 posicaoMira = impacto.point - physics.position;
            posicaoMira.y = 0;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMira);

            physics.MoveRotation(novaRotacao);
        }
    }

    public void TomarDano(int dano)
    {
        Vida -= dano;

        ScriptControlaUI.AtualizaSliderVidaJogador();

        if (Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }
    }

    private void EncostandoParede()
    {
        float maxDistance = 1;

        encostandoForward = Physics.Raycast(transform.position, Vector3.forward, maxDistance, MascaraParede);
        encostandoRight = Physics.Raycast(transform.position, Vector3.right, maxDistance, MascaraParede);
        encostandoBack = Physics.Raycast(transform.position, Vector3.back, maxDistance, MascaraParede);
        encostandoLeft = Physics.Raycast(transform.position, Vector3.left, maxDistance, MascaraParede);
    }

    private void AjustaDirecao()
    {
        if (encostandoForward && direcao.z > 0)
        {
            direcao.z = 0;
        }

        if (encostandoRight && direcao.x > 0)
        {
            direcao.x = 0;
        }

        if (encostandoBack && direcao.z < 0)
        {
            direcao.z = 0;
        }

        if (encostandoLeft && direcao.x < 0)
        {
            direcao.x = 0;
        }
    }
}
