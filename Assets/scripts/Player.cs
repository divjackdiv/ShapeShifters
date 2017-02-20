using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	public int team; //Team 1 = red, Team 2 = blue
	bool fading = false;
	public bool gotFlag = false;
	float eventTime;
	private GameObject manager;
    public GameObject ennemyFlag;
    public Vector3 spawnPoint;


    void Start ()
	{
        spawnPoint = transform.position;
        manager = GameObject.Find("GameManager");
		if(manager.GetComponent<GameManager>().bluePlayers <= manager.GetComponent<GameManager>().redPlayers)
		{
            team = 2;
            manager.GetComponent<GameManager>().bluePlayers++;

        }
		else
		{
            team = 1; 
            manager.GetComponent<GameManager>().redPlayers++;

        }
    
        if (team == 1) ennemyFlag = GameObject.Find("BlueFlag");
        else ennemyFlag = GameObject.Find("RedFlag");
    }

	void Update () 
	{
		if(fading == true)
		{
			eventTime -= (Time.deltaTime / 2);
			if(eventTime <= 0)
			{
				fading = false;
			}
		}
	}

	void OnDestroy()
        {
            if (gotFlag)
            {
                ennemyFlag.gameObject.SetActive(true);
            }
        }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag != "flag") return;
        if (coll.gameObject == ennemyFlag)
        {
            gotFlag = true;
            GetComponent<Sounds>().CmdPlaySound(0);
            if (team == 1) manager.GetComponent<GameManager>().blueFlagTaken = true;
            else manager.GetComponent<GameManager>().redFlagTaken = true;
            coll.gameObject.SetActive(false);
        }
        if (coll.gameObject != ennemyFlag && gotFlag)
        {
            gotFlag = false;
            manager.GetComponent<GameManager>().score = team ;
            GetComponent<Sounds>().CmdPlaySound(1);
            ennemyFlag.gameObject.SetActive(true);
        }
    }

}