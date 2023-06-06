using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public int ProbabilidadeSpawn;
    public float TempoSpawn;
    public float DistanciaSpawnMax;
    public float DistanciaJogadorMin;
    public LayerMask MascaraColisores;
    public GameObject Zumbi;
    private float contadorSpawn = 0;
    private float distanciaJogador;
    private GameObject jogador;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DistanciaSpawnMax);
    }

    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }

    void Update()
    {
        distanciaJogador = Vector3.Distance(jogador.transform.position, transform.position);

        contadorSpawn += Time.deltaTime;
        int numeroGerado = Random.Range(1, 100);

        if (contadorSpawn >= TempoSpawn)
        {
            StartCoroutine(Spawn(numeroGerado));

            contadorSpawn = 0;
        }
    }

    private IEnumerator Spawn(int numeroGerado)
    {
        if (numeroGerado <= ProbabilidadeSpawn && distanciaJogador >= DistanciaJogadorMin)
        {
            Vector3 posicaoAleatoria = AleatorizarPosicao();
            Collider[] colisores = Physics.OverlapSphere(posicaoAleatoria, 1, MascaraColisores);

            while (colisores.Length > 0)
            {
                posicaoAleatoria = AleatorizarPosicao();
                colisores = Physics.OverlapSphere(posicaoAleatoria, 1, MascaraColisores);
                yield return null;
            }

            Instantiate(Zumbi, posicaoAleatoria, transform.rotation);
        }
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * DistanciaSpawnMax;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
}
