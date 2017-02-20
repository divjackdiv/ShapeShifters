using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Gui : MonoBehaviour {

    public Text timerText;
    public Text eventText;
    float eventTime;
    bool fadingEvent;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
/*
        if (fadingEvent == true)
        {
            eventTime -= (Time.deltaTime / 2);
            if (eventTime <= 0)
            {
                fadingEvent = false;
            }
            //fade(eventText);
        }*/
    }

    void onDestroy()
    {
		//eventText.text = "<color=#000000FF>You Died</color>";
        //eventTime = 2;
        //fading = true;
    }
   /* public void fade(Text t)
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        temp = t.material.color;
        temp.a = eventTime;
        t.material.color = temp;
    }*/

}
/*		if(Input.GetKey(KeyCode.K))
{
eventText.text = "<color=#000000FF>You Killed </color><color=#4576BAFF>TheWorstPlayer</color>";
eventText.text = "<color=#000000FF>You were killed by </color><color=#4576BAFF>TheBestPlayer</color>";
eventTime = 2;
fading = true;
}
temp = eventText.material.color;
temp.a = eventTime;
eventText.material.color = temp;*/
