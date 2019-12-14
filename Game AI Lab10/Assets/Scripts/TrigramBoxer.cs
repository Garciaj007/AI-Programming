using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigramBoxer : MonoBehaviour {
    public enum State { READY, HIT_LEFT, HIT_RIGHT, MISS_LEFT, MISS_RIGHT}
    State boxerState = State.READY;

    // Create a trigram predictor with 10 data sequence
    TrigramPredictor predictor = new TrigramPredictor(10);
    
    SpriteRenderer spriteRenderer;
    public Sprite spriteReady;
    public Sprite spriteHitLeft;
    public Sprite spriteHitRight;
    public Sprite spriteMissLeft;
    public Sprite spriteMissRight;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
    
        // Predict the next action
        char nextAction = predictor.GetNextPrediction();
        
        boxerState = !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) ? State.READY : boxerState;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            predictor.InputTrainingData('R');
            
            if (nextAction == 'R')
                boxerState = State.MISS_RIGHT;
            else
                boxerState = State.HIT_RIGHT;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            predictor.InputTrainingData('L');
            
            if (nextAction == 'L')
                boxerState = State.MISS_LEFT;            
            else
                boxerState = State.HIT_LEFT;
        }
                
        Display(); // Display the current state
    }

    void Display()
    {
        switch (boxerState)
        {
            case State.READY:
                spriteRenderer.sprite = spriteReady;
                break;
            case State.HIT_LEFT:
                spriteRenderer.sprite = spriteHitLeft;
                break;
            case State.HIT_RIGHT:
                spriteRenderer.sprite = spriteHitRight;
                break;
            case State.MISS_LEFT:
                spriteRenderer.sprite = spriteMissLeft;
                break;
            case State.MISS_RIGHT:
                spriteRenderer.sprite = spriteMissRight;
                break;
        }
    }
}
