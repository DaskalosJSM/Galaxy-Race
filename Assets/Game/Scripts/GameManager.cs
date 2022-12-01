using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    principalMenu,
    Game,
    gameOver,
    Credits,
    Controls
}

public class GameManager : MonoBehaviour
{
    // Inicializo el singleton en el primer script 
    public static GameManager sharedInstance;

    // Declaración del estado del juego
    public GameState currentGameState = GameState.principalMenu;

    public void Awake()
    {
        // que despierte y enfatizo con el siguiente fragmento
        // Singleton
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Función encargado de iniciar la scena menú principal
    public void PrincipalMenu()
    {
        SetGameState(GameState.principalMenu);
    }
    // Función encargado de iniciar la scena créditos
    public void Credits()
    {
        SetGameState(GameState.Credits);
    }
    // Función encargado de iniciar la scena Game
    public void Game()
    {
        SetGameState(GameState.Game);
    }
    // Función encargado de iniciar la scena controls
    public void Controls()
    {
        SetGameState(GameState.Controls);
    }
    // Función encargado de iniciar la scena de final de juego
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void SetGameState(GameState newGameState)
    {
        this.currentGameState = newGameState;

        if (newGameState == GameState.principalMenu)
        {
            //TODO: colocar la logica del menu
            SceneManager.LoadScene("PrincipalMenu");
        }
        else if (newGameState == GameState.Game)
        {
            //TODO: colocar la logica del level 5
            SceneManager.LoadScene("Game");
            SceneManager.LoadScene("MapCircuit", LoadSceneMode.Additive);
            SceneManager.LoadScene("PlayerMovement", LoadSceneMode.Additive);
            SceneManager.LoadScene("UI Elements", LoadSceneMode.Additive);
        }
        else if (newGameState == GameState.Credits)
        {
            //TODO: colocar la logica del level 5
            SceneManager.LoadScene("Credits");
        }
        else if (newGameState == GameState.Controls)
        {
            //TODO: colocar la logica del level 5
            SceneManager.LoadScene("Controls");
        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO: colocar la logica del gameOver
            SceneManager.LoadScene("GameOver");
        }
    }
}
