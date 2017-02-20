using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Navigate : MonoBehaviour 
{
    public List<GameObject> targets;
    private int size;
    public int index;
	float dist;

	public NavMeshAgent agent;
    //Mode 1 a to b, b to c ect
    //Mode 2 a to b fast, then stop for a bit, b to c fast, wait, ect
	float mode = 1;
	float t = 0;


	void Start () 
	{		
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination(targets[index].transform.position);
		gameObject.GetComponent<NavMeshAgent>().speed = 5;
		gameObject.GetComponent<NavMeshAgent>().acceleration = 25;
        size = targets.Count;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			mode = 1;
		}
		if(Input.GetKeyDown(KeyCode.W))
		{
			mode = 2;
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			mode = 3;
		}
        
        dist = Vector3.Distance (transform.position, targets[index].transform.position);

		if(mode == 1)
		{
			gameObject.GetComponent<NavMeshAgent>().speed = 5;
			gameObject.GetComponent<NavMeshAgent>().acceleration = 25;
            if (dist < 1)
            {
                index++;
                if (index >= size) index = 0;
                agent.SetDestination(targets[index].transform.position);
            }
		}

		if(mode == 2)
		{
			gameObject.GetComponent<NavMeshAgent>().speed = 10;
			gameObject.GetComponent<NavMeshAgent>().acceleration = 50;	
			if (dist < 1) 
			{
				t += Time.deltaTime;
				if(t > 3)
				{
					t = 0;
                    index++;
                    if (index >= size) index = 0;
                    agent.SetDestination (targets[index].transform.position);
				}
			}
         }

		if(mode == 3)
		{

		}
	}
}