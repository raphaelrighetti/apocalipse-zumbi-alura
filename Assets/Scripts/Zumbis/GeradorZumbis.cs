using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    public float TempoSpawn;

    public int ProbabilidadeSpawn;

    public GameObject Zumbi;

    private float contador = 0;

    void Start()
    {

    }


    void Update()
    {
        contador += Time.deltaTime;
        int numeroGerado = Random.Range(1, 100);

        if (contador >= TempoSpawn)
        {
            Spawn(numeroGerado);

            contador = 0;
        }
    }

    void Spawn(int numeroGerado)
    {
        if (numeroGerado <= ProbabilidadeSpawn)
        {
            Instantiate(Zumbi, transform.position, transform.rotation);
        }
    }
}
