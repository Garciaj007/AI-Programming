using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigramPredictor {
    int maxDataLength;    
    List<char> data = new List<char>();

    Dictionary<string, int> patternCounter = new Dictionary<string, int>();
	
    public BigramPredictor(int length)
    {
        maxDataLength = length;

        // initialize pattern counters;
        patternCounter["LL"] = 0;
        patternCounter["LR"] = 0;
        patternCounter["RL"] = 0;
        patternCounter["RR"] = 0;
    }

    public char GetNextPrediction()
    {
        char prediction;

        if (data.Count > 1)
        {
            char lastElement = data[data.Count - 1];

            // To do : Get the number of L (and R respectively) after the last element from the patternCounter dictionary
            int numL = 0;
            int numR = 0; 
            
            int total = numL + numR;

            // To do : calculate probabilities to get L and R 
            float probabilityToGetL = numL / total;
            float probabilityToGetR = numR / total;


            if (total > 0)
            {
                if (probabilityToGetL > probabilityToGetR)
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
        if (data.Count > 0)
        {
            // update the counter with the new input value correspondingly
            char lastElement = data[data.Count - 1];
            patternCounter[lastElement.ToString() + newValue.ToString()] ++;
        }
        data.Add(newValue);

        while(data.Count > maxDataLength)
        {
            // update the counter with the deleted value correspondingly
            patternCounter[data[0].ToString() + data[1].ToString()] --;
            data.RemoveAt(0); // remove head
        }
    }
}
