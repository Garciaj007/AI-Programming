using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeCell : MonoBehaviour {
    public GameObject O_Mark;
    public GameObject X_Mark;

    public enum MarkType { NONE, O_MARK, X_MARK};
    MarkType currentMark;

    GameObject ticTacToeBoard;
	// Use this for initialization
	void Start () {
        ticTacToeBoard = transform.parent.gameObject;
        DisplayMark(MarkType.NONE);
	}
	
    public int GetCurrentMark()
    {
        return (int)currentMark;
    }

    public void Reset()
    {
        DisplayMark(MarkType.NONE);
    }

    void OnMouseDown()
    {
        if (currentMark == MarkType.NONE)
        {            
            DisplayMark(MarkType.O_MARK);

            ticTacToeBoard.SendMessage("OnCellClicked", this);
        }
    }

    public void DisplayMark(MarkType mark)
    {
        currentMark = mark;
        switch (mark)
        {
            case MarkType.NONE:
                O_Mark.SetActive(false);
                X_Mark.SetActive(false);
                break;
            case MarkType.O_MARK:
                O_Mark.SetActive(true);
                X_Mark.SetActive(false);
                break;
            case MarkType.X_MARK:
                O_Mark.SetActive(false);
                X_Mark.SetActive(true);
                break;
        }        
    }
}
