using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlaUI : MonoBehaviour
{
    public string TextoTempoSobrevivencia;
    public string TextoMelhorTempo;
    public TMP_Text TMP_TextoTempoSobrevivencia;
    public TMP_Text TMP_TextoMelhorTempo;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    private float melhorTempo;
    private StatusPersonagem status;

    void Start()
    {
        status = GameObject.FindWithTag("Jogador").GetComponent<StatusPersonagem>();
        melhorTempo = PlayerPrefs.GetFloat("MelhorTempo", 0);

        SliderVidaJogador.maxValue = status.Vida;
        AtualizaSliderVidaJogador();
    }

    public void AtualizaSliderVidaJogador()
    {
        SliderVidaJogador.value = status.Vida;
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        PainelGameOver.SetActive(true);

        TMP_TextoTempoSobrevivencia.text = GetTextoGameOver();
        TMP_TextoMelhorTempo.text = GetTextoMelhorTempo();
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

    private string GetTextoGameOver()
    {
        int minutos = (int)Time.timeSinceLevelLoad / 60;
        int segundos = (int)Time.timeSinceLevelLoad % 60;

        return $"\n{TextoTempoSobrevivencia} {minutos}min e {segundos}s";
    }

    private string GetTextoMelhorTempo()
    {
        AjustaMelhorTempo();

        int minutos = (int)melhorTempo / 60;
        int segundos = (int)melhorTempo % 60;

        return $"{TextoMelhorTempo} {minutos}min e {segundos}s";
    }

    private void AjustaMelhorTempo()
    {
        if (Time.timeSinceLevelLoad > melhorTempo)
        {
            melhorTempo = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("MelhorTempo", Time.timeSinceLevelLoad);
        }
    }
}
