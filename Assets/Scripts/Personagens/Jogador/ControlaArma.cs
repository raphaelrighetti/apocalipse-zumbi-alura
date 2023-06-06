using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public float CadenciaTiro;
    public AudioClip SomTiro;
    public GameObject Bala;
    public GameObject CanoDaArma;
    private float contador;

    void Update()
    {
        GameObject balaRef = null;

        if (Input.GetButtonDown("Fire1"))
        {
            contador = CadenciaTiro;
        }

        if (Input.GetButton("Fire1"))
        {
            if (contador >= CadenciaTiro)
            {
                balaRef = Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);

                ControlaAudio.instancia.PlayOneShot(SomTiro);

                contador = 0;

                return;
            }

            contador += Time.deltaTime;
        }

        if (balaRef != null)
        {
            Destroy(balaRef, 3);
        }
    }
}
