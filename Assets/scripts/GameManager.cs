using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    [SyncVar]
	int timer;

    [SyncVar]
	public int bluePlayers;
    [SyncVar]
    public int redPlayers;

	float t;
    [SyncVar]
	float redPoints;
    [SyncVar]
    float bluePoints;
    public int score; // If is set to 1, red team has scored recently, if 2 blue team has scored recently, else no one score RECENTLY
	public Text timerText;
	public Text eventText;
    public Text BlueFlagTaken;
    public Text RedFlagTaken;
    public Text redScore;
	public Text blueScore;
	public Text redFlagInfo;
	public Text blueFlagInfo;
	float eventTime;
	bool fadingEvent;
    int index = 0;

	public bool redFlagTaken;
	public bool blueFlagTaken;

	Color temp;


	void Start () 
	{
        redPlayers = 0;
        bluePlayers = 0;
        score = 0;
        bluePoints = 0;
        redPoints = 0;
        timer = 600;
	}

	void Update () 
	{
        TimerGui();
        redScore.text = "" + redPoints;
        blueScore.text = "" + bluePoints;

        if (fadingEvent == true)
        {
            eventTime -= (Time.deltaTime / 2);
            if (eventTime <= 0)
            {
                fadingEvent = false;
            }
            Fade(eventText);
        }
        if (score > 0)
        {
            Score(score);
        }
        if(redFlagTaken || blueFlagTaken)
        {
            StolenFlag();
        }
    }

    public void TimerGui()
    {
        t += Time.deltaTime;
        if (t > 1)
        {
            t = 0;
            timer--;
        }

        int minutes = timer / 60;
        int seconds = timer % 60;

        if (seconds < 10)
        {
            timerText.text = "" + minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = "" + minutes + ":" + seconds;
        }
    }

    public void Score(int i)
    {
        if (i == 1)
        {
            redPoints++;
            blueFlagTaken = false;
            BlueFlagTaken.text = "";
            eventText.text = "<color=#DE3250FF>Red Team Score</color>";
        }
        else if(i == 2) {
            bluePoints++;
            redFlagTaken = false;
            RedFlagTaken.text = "";
            eventText.text = "<color=#0000cc>Blue Team Score</color>";
        }
        score = 0;
        fadingEvent = true;
        eventTime = 2;
    }

    public void StolenFlag()
    {
        if (redFlagTaken)
        {
            RedFlagTaken.text = "<color=#DE3250FF>Red Flag Has Been Taken !</color>";
        }
        if (blueFlagTaken)
        {
            BlueFlagTaken.text = "<color=#0000cc>Blue Flag Has Been Taken !</color>";
        }
    }

    public void Fade(Text t)
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        temp = t.material.color;
        temp.a = eventTime;
        t.material.color = temp;
    }
}