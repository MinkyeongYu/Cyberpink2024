using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    static GameManager Instance;
    public static GameManager GetInstance() { Init(); return  Instance; }
    public GameState currentGameState = GameState.MENU;
    
    // 나중에 사용할지도 모르니..
    // InputManager _input = new InputManager();
    // public static InputManager Input { get { return GetInstance()._input; } }

    static void Init() 
    {
        if(Instance == null) {
            GameObject go = GameObject.Find("@GameManager");
            if(go == null) {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            Instance = go.GetComponent<GameManager>();
        }
    }
    void Awake() 
    {
        Instance = this;
        GameManager[] obj = FindObjectsOfType<GameManager>();
        if(obj.Length == 1) {
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
    }
    void Start() 
    {
        Init();
        StartGame();
    }
    void Update() 
    {
        
    }
    public void StartGame() 
    {
        SetGameState(GameState.INGAME);
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
