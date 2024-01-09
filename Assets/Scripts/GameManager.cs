using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GameState 
{
    MENU,
    INGAME, 
    ISGAMEOVER,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState = GameState.MENU;

    void Awake() 
    {
        instance = this;
        
        GameManager[] obj = FindObjectsOfType<GameManager>();
        if(obj.Length == 1) {
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
    }
    public void StartGame() 
    {
        SetGameState(GameState.INGAME);
    }
    void Start() 
    {
        StartGame();
    }
    void SetGameState(GameState newGameState) 
    {
        if(newGameState == GameState.MENU)
        {
            currentGameState = GameState.MENU;
        }
        else if(newGameState == GameState.INGAME) 
        {
            currentGameState = GameState.INGAME;
        }
        else if(newGameState == GameState.ISGAMEOVER)
        {
            currentGameState = GameState.ISGAMEOVER;
        }
    }

}
