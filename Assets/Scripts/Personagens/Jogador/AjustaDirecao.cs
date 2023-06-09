using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustaDirecao : MonoBehaviour
{
    public float DistanciaMaxima;
    public LayerMask MascaraZumbi;

    public Vector3 Ajustar(Vector3 posicao, Vector3 direcao)
    {
        posicao.y = 1;

        bool encostandoForward = Physics.Raycast(posicao, Vector3.forward, DistanciaMaxima, ~MascaraZumbi);
        bool encostandoRight = Physics.Raycast(posicao, Vector3.right, DistanciaMaxima, ~MascaraZumbi);
        bool encostandoBack = Physics.Raycast(posicao, Vector3.back, DistanciaMaxima, ~MascaraZumbi);
        bool encostandoLeft = Physics.Raycast(posicao, Vector3.left, DistanciaMaxima, ~MascaraZumbi);

        if (encostandoForward && direcao.z > 0)
        {
            direcao.z = 0;
        }

        if (encostandoRight && direcao.x > 0)
        {
            direcao.x = 0;
        }

        if (encostandoBack && direcao.z < 0)
        {
            direcao.z = 0;
        }

        if (encostandoLeft && direcao.x < 0)
        {
            direcao.x = 0;
        }

        return direcao;
    }
}
