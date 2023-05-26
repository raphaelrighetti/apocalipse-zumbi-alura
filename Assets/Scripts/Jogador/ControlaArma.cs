using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{

    public AudioClip SomTiro;

    public GameObject Bala;

    public GameObject CanoDaArma;

    void Update()
    {
        GameObject balaRef = null;

        if (Input.GetButtonDown("Fire1"))
        {
            balaRef = Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(SomTiro);
        }

        if (balaRef != null)
        {
            Destroy(balaRef, 3);
        }
    }
}
