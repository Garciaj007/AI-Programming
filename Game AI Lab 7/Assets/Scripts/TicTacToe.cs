using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe {
    public enum ResultType {NONE, COMPUTER, HUMAN, DRAW }
    bool useSmartEvaluationFunction = false;
    public class Node
    {
        public const int MAX_CELL = 9;
        public int[] cells = new int[MAX_CELL] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; 

        public int player; // 1 = human player, -1 = computer(AI) player
        
        public Node()
        {
            player = 0;
        }

        public Node(Node t)
        {
            cells = new int[MAX_CELL];
            for (int i = 0; i < MAX_CELL; i++)
            {
                cells[i] = t.cells[i];
            }
        }

        public void Clear()
        {
            for (int i = 0; i < MAX_CELL; i++)
            {
                cells[i] = 0;
            }
        }

        public bool IsTheSameAs(Node n)
        {
            bool result = true;
            for (int i = 0; i < MAX_CELL; i++)
            {
                if (cells[i] !=  n.cells[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public void CopyFrom(Node n)
        {
            for (int i = 0; i < MAX_CELL; i++)
            {
                cells[i] = n.cells[i];                
            }
        }
    }

    public TicTacToe(bool useSmartEvaluation)
    {
        useSmartEvaluationFunction = useSmartEvaluation;
    }
    
    public int MakeComputerDecision(Node node, int searchDepth)
    {
        Node selected = null;
        MinMax(node, searchDepth, ref selected);

        for (int i=0; i<Node.MAX_CELL; i++)
        {
            if (node.cells[i] != selected.cells[i])
                return i;
        }

        Debug.Log("Error");
        return -1; // if it reaches here, then error
    }

    int MinMax(Node node, int searchDepth, ref Node selected)
    {        
        if (searchDepth <= 0)
        {
            // positive values are good for the maximizing player
            // negative values are good for the minimizing player
            return useSmartEvaluationFunction ? Evaluate2(node) : Evaluate(node);
        }

        // maximizing player is (+1)
        // minimizing player is (-1)
        // To do: initialize 'alpha' with a proper value
        int alpha = 0;
        
        Node child = GetNextChild(node, null);        
        while (child != null)
        {
            Node notUsed = null;
            int score = MinMax(child, searchDepth - 1, ref notUsed);

            // To do: update the alpha value properly
            // refer to the algorithm in the lecture slide

            // To do: remove the following line after implementing this part.
            child = null;
        }

        return alpha;
    }

    Node GetNextChild(Node node, Node PrevChild)
    {
        List<Node> childNodes = new List<Node>();
        // Generate all possible child nodes from the input node
        for (int i = 0; i < Node.MAX_CELL; i++)
        {
            if (node.cells[i] == 0)
            {
                Node child = new Node();
                child.CopyFrom(node);
                if (node.player == 1)
                {
                    child.cells[i] = 2;
                    child.player = -1;
                }
                else
                {
                    child.cells[i] = 1;
                    child.player = 1;
                }

                childNodes.Add(child);
            }
        }

        Node nextChild = null;
        if (childNodes.Count > 0)
        {
            if (PrevChild == null)
                nextChild = childNodes[0];
            else
            {
                int count = childNodes.Count;
                
                for (int i = 0; i < count; i++)
                {
                    if (childNodes[i].IsTheSameAs(PrevChild) && (i + 1 < count))
                    {
                        nextChild = childNodes[i + 1];
                        break;
                    }
                }
            }
        }
        return nextChild;
    }
    
    // Evaluation Function for 7.1 TicTacToe    
    int Evaluate(Node node)
    {        
        // Utility function
        // E(n) = M(n) - O(n)
        // M(n) is the total of My possible winning lines
        int possibleWinningLinesForO = GetPossibleWinningLines(node, 1);
        // O(n) is the total of opponent's possible winning lines
        int possibleWinningLinesForX = GetPossibleWinningLines(node, 2);

        return possibleWinningLinesForO - possibleWinningLinesForX;
        
    }
    int GetPossibleWinningLines(Node node, int ox)
    {
        // To do : Implement this function using the GetWinningLines() function
        // This is for 7.1
        return 0;
    }

    int GetWinningLines(Node node, int ox)
    {
        int[,] linePatterns = {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };

        int numWinning = 0;
        for (int i = 0; i < linePatterns.GetLength(0); i++)
        {
            int cell1 = linePatterns[i, 0];
            int cell2 = linePatterns[i, 1];
            int cell3 = linePatterns[i, 2];

            if (node.cells[cell1] == ox && node.cells[cell2] == ox && node.cells[cell3] == ox )
            {
                numWinning++;
            }
        }

        return numWinning;
    }

    // Evaluation Function for 7.2 TicTacToe (Smarter) 
    int Evaluate2(Node node)
    {
        int[,] linePatterns = {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };
        int score = 0;    
        for (int i = 0; i < linePatterns.GetLength(0); i++)
        {
            int cell1 = linePatterns[i, 0];
            int cell2 = linePatterns[i, 1];
            int cell3 = linePatterns[i, 2];
            score += EvaluateLine(node.cells[cell1], node.cells[cell2], node.cells[cell3], node.player);
        }
        return score;
    }

    int EvaluateLine(int c1, int c2, int c3, int player)
    {
        int value = 0;
        // To do: Implement this function
        // This is for 7.2

        return value;
    }




    public bool DoesGameFinish(Node node, ref ResultType result)
    {
        if (GetWinningLines(node, 1) > 0)
        {
            // Human won
            result = ResultType.HUMAN;
            return true;
        }

        if (GetWinningLines(node, 2) > 0)
        {
            // AI won
            result = ResultType.COMPUTER;
            return true;
        }

        bool isThereEmptyCell = false;
        foreach (var cell in node.cells)
        {
            if (cell == 0)
            {
                isThereEmptyCell = true;
                break;
            }
        }
        if (!isThereEmptyCell)
        {
            // Draw
            result = ResultType.DRAW;
            return true;
        }


        return false;
    }

}
