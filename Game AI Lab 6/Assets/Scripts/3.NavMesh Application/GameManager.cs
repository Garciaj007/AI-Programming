using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] spawningPoints;
    public GameObject alien;
    public GameObject player;
	// Use this for initialization
	IEnumerator Start () {
		while (true)
        {
            // To do : Select one of the spawning points(spawningPoints) randomly
            // and then instantiate an alien object
            // Don't forget to set the alien's target to player
            
            yield return new WaitForSeconds(5f);
        }
	}
}
