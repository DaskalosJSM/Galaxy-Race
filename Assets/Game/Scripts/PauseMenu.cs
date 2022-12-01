using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
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

    }
    public void Abandonar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PrincipalMenu");
    }
}

