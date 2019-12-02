using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeBoard : MonoBehaviour {
    public TicTacToeCell[] cells;
    public int maxSearchDepth = 2;
    public bool useSmartEvaluationFunction = false;
        
    TicTacToe miniMax;
    TicTacToe.ResultType result = TicTacToe.ResultType.NONE;

    void Awake()
    {
        miniMax = new TicTacToe(useSmartEvaluationFunction);
    }

    public TicTacToe.ResultType GetGameResult()
    {
        return result;
    }
    	
    TicTacToe.Node GenerateNodeFromBoard()
    {
        TicTacToe.Node node = new TicTacToe.Node();
        for (int i=0; i<cells.Length; i++)
        {
            node.cells[i] = cells[i].GetCurrentMark();
        }
        return node;
    }

    void OnCellClicked(TicTacToeCell cell)
    {        
        //Debug.Log(GetCellIndex(cell));
     
        TicTacToe.Node node = GenerateNodeFromBoard();
        node.player = 1;        
        if (miniMax.DoesGameFinish(node, ref result))
        {
            return;
        }
                
        int cellIndexChosenByComputer = miniMax.MakeComputerDecision(node, maxSearchDepth);
        cells[cellIndexChosenByComputer].DisplayMark(TicTacToeCell.MarkType.X_MARK);

        node = GenerateNodeFromBoard();
        if (miniMax.DoesGameFinish(node, ref result))
        {
            return;
        }        
    }

    int GetCellIndex(TicTacToeCell cell)
    {
        for (int index = 0; index < cells.Length; index++)
        {
            if (cells[index] == cell)
                return index;
        }
        return -1; // Not found
    }

    void Reset(bool doesHumanPlayerDoFirst )
    {
        foreach (var cell in cells)
        {
            cell.Reset();
        }
        
        result = TicTacToe.ResultType.NONE;

        if (!doesHumanPlayerDoFirst)
        {
            TicTacToe.Node node = GenerateNodeFromBoard();
            node.player = 1;
            int cellIndexChosenByComputer = miniMax.MakeComputerDecision(node, maxSearchDepth);
            cells[cellIndexChosenByComputer].DisplayMark(TicTacToeCell.MarkType.X_MARK);            
        }
    }
}
