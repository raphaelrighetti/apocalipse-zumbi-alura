using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float velocidade;

    private Rigidbody physics;

    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    void Update()
    {
        physics.MovePosition(physics.position + (transform.forward * (velocidade * Time.deltaTime)));
    }
}
