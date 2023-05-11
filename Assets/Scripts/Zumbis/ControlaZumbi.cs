using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{

    public GameObject Jogador;

    public float velocidade;

    private Rigidbody physics;

    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direcao = Jogador.transform.position - physics.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);

        float distancia = Vector3.Distance(physics.position, Jogador.transform.position);

        if (distancia > 2.5)
        {
            physics.MovePosition(physics.position + (direcao.normalized * (velocidade * Time.deltaTime)));

            physics.MoveRotation(novaRotacao);
        }
    }
}
