using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    public GameObject Jogador;
    private Vector3 distanciaCompensar;

    void Start()
    {
        distanciaCompensar = transform.position - Jogador.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Jogador.transform.position + distanciaCompensar;
    }
}
