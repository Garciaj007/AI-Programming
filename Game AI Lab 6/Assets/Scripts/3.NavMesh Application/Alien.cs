using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public GameObject target;

    // Use this for initialization
    IEnumerator Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();


        while (true)
        {
            if (target != null)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }*/
        if (target != null)
        {
         //   navMeshAgent.SetDestination
        }
    }
}
