using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{

    public float Velocidade;

    public bool Vivo;

    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

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

        if (direcao != Vector3.zero)
        {
            animator.SetBool("Movendo", true);
        }
        else
        {
            animator.SetBool("Movendo", false);
        }

        if (!Vivo)
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
            posicaoMira.y = physics.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMira);

            physics.MoveRotation(novaRotacao);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Vivo = false;
        TextoGameOver.SetActive(true);
    }
}
