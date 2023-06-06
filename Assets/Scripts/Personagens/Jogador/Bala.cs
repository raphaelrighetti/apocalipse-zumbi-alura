using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade;
    private int dano;
    private Rigidbody rb;

    void Start()
    {
        dano = GameObject.FindWithTag("Jogador").GetComponent<StatusPersonagem>().Dano;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (transform.forward * (Velocidade * Time.deltaTime)));
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            objetoDeColisao.GetComponent<ControlaZumbi>().TomarDano(dano);
        }

        Destroy(gameObject);
    }
}
