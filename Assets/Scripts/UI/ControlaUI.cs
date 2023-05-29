using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaUI : MonoBehaviour
{

    public Slider SliderVidaJogador;

    private StatusPersonagem status;

    void Start()
    {
        status = GameObject.FindWithTag("Jogador").GetComponent<StatusPersonagem>();

        SliderVidaJogador.maxValue = status.Vida;
        AtualizaSliderVidaJogador();
    }

    public void AtualizaSliderVidaJogador()
    {
        SliderVidaJogador.value = status.Vida;
    }
}
