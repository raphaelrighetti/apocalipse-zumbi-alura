using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float Velocidade;

    private Rigidbody physics;

    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    void Update()
    {
        physics.MovePosition(physics.position + (transform.forward * (Velocidade * Time.deltaTime)));
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
        }

        Destroy(gameObject);
    }
}
