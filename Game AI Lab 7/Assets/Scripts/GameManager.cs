using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject ticTacToe;
    
    bool doesHumanPlayerDoFirst;

    public enum GameState { INIT, PLAY, END}
    GameState state;

	// Use this for initialization
	void Start () {
        SetState(GameState.INIT);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        switch(state)
        {
            case GameState.INIT:                
                GUI.Box(new Rect(10, 10, 200, 100), "Do you want to play first ?");

                if (GUI.Button(new Rect(20, 50, 80, 50), "Yes"))
                {
                    doesHumanPlayerDoFirst = true;                    
                    SetState(GameState.PLAY);
                }
                else if (GUI.Button(new Rect(110, 50, 80, 50), "No"))
                {
                    doesHumanPlayerDoFirst = false;                    
                    SetState(GameState.PLAY);
                }
                break;
            case GameState.PLAY:
                if (ticTacToe.GetComponent<TicTacToeBoard>().GetGameResult() != TicTacToe.ResultType.NONE)
                {
                    state = GameState.END;
                }
                break;
            case GameState.END:
                if (ticTacToe.GetComponent<TicTacToeBoard>().GetGameResult() == TicTacToe.ResultType.HUMAN)
                {
                    GUI.Box(new Rect(10, 10, 200, 100), "You won !");
                }
                else if (ticTacToe.GetComponent<TicTacToeBoard>().GetGameResult() == TicTacToe.ResultType.COMPUTER)
                {
                    GUI.Box(new Rect(10, 10, 200, 100), "AI won !");
                }
                else
                {
                    GUI.Box(new Rect(10, 10, 200, 100), "Draw !");
                }
                if (GUI.Button(new Rect(20, 50, 160, 50), "Restart ?"))
                {                    
                    SetState(GameState.INIT);
                }
                break;
        }
    }

    void SetState(GameState s)
    {
        state = s;
        switch(s)
        {
            case GameState.INIT:
                ticTacToe.SetActive(false);
                break;
            case GameState.PLAY:                
                ticTacToe.SetActive(true);
                ticTacToe.SendMessage("Reset", doesHumanPlayerDoFirst);
                break;
            case GameState.END:
                break;

        }
    }
}
