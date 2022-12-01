using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public StatsManager statsManager;
    [SerializeField] GameObject menuPausa;
    private void Awake()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa();
        }
    }
    public void Pausa()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }
    public void Reanudar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
        SceneManager.LoadScene("MapCircuit", LoadSceneMode.Additive);
        SceneManager.LoadScene("PlayerMovement", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI Elements", LoadSceneMode.Additive);
        statsManager.score = 0;
        statsManager.health = 100;
        statsManager.turbo = 0;
        statsManager.timeScore = 0;

    }
    public void Abandonar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PrincipalMenu");
    }
}

