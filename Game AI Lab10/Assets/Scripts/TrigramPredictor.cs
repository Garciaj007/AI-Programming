using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrigramPredictor
{
    int maxDataLength;
    List<char> data = new List<char>();

    Dictionary<string, int> patternCounter = new Dictionary<string, int>();

    public TrigramPredictor(int length)
    {
        maxDataLength = length;

        // To do : Initialize pattern counters for the 8 cases
        // LLL, LLR, LRL, LRR, RLL, RLR, RRL, RRR        
        patternCounter["LLR"] = 0;
        patternCounter["LLR"] = 0;
        patternCounter["LRL"] = 0;
        patternCounter["LRR"] = 0;
        patternCounter["RLL"] = 0;
        patternCounter["RLR"] = 0;
        patternCounter["RRL"] = 0;
        patternCounter["RRR"] = 0;
    }

    public char GetNextPrediction()
    {
        char prediction;
        // To do : Compelete this function by referencing BigramPredictor's GetNextPrediction()
        //         You need to consider the second last element as well 
        
        if (data.Count > 2)
        {
            //char lastElem = data.Last();
            //char secondLastElem = data[data.Count - 2];

            int numL = data.Where(c => c == 'L').Count();
            int numR = data.Where(c => c == 'R').Count();
            int total = numL + numR;

            float probabilityL = numL / total;
            float probabilityR = numR / total;

            if (total > 0)
            {
                if (probabilityL > probabilityR)
                {
                    prediction = 'L';
                }
                else
                {
                    prediction = 'R';
                }
                return prediction;
            }
        }

        // if prediction is not decided by the Ngram predictor then use random function.
        prediction = Random.Range(0, 2) < 1 ? 'L' : 'R';
        return prediction;
    }

    public void InputTrainingData(char newValue)
    {
        // To do : Complete this function by referencing BigramPredictor's InputTrainingData()
        //         Don't forget this is Trigram so you need to consider the second last element for updating pattern counters when adding a new value or removing the head value.
        if (data.Count > 1)
        {
            char lastElement = data.Last();
            char secondLastElem = data[data.Count - 2];
            patternCounter[lastElement.ToString() + secondLastElem.ToString() + newValue.ToString()] ++;
        }
        data.Add(newValue);

        while (data.Count > maxDataLength)
        {
            patternCounter[data[0].ToString() + data[1].ToString() + data[2].ToString()] --;
            data.RemoveAt(0);
        }
    }
}
