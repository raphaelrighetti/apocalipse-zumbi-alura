using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoPersonagem : MonoBehaviour
{

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        rb.MovePosition(rb.position + (direcao * (velocidade * Time.deltaTime)));
    }

    public void Rotacionar(Vector3 posicaoRotacao)
    {
        posicaoRotacao.y = 0;
        Quaternion novaRotacao = Quaternion.LookRotation(posicaoRotacao);

        rb.MoveRotation(novaRotacao);
    }
}
