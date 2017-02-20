using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Morphing : MonoBehaviour 
{
	public GameObject shape1;
	public GameObject shape2;
	int currentForm = 1;
	RaycastHit ground;
	public Image selector;
	public Image cdBar;
	bool cd = true;
	bool barCharging  = false;
	float barAmount = 1;

	void Start () 
	{
		GetComponent<MeshFilter>().sharedMesh = shape1.GetComponent<MeshFilter>().sharedMesh;
	}
	
	void Update () 
	{
		if(barCharging == true)
		{
			barAmount += (Time.deltaTime / 5);
			if(barAmount > 1)
			{
				barCharging = false;
			}
		}

		if(Input.GetButton("FormChange") && cd == true)
		{
			cd = false;
			barAmount = 0;
			StartCoroutine("Shrink");
		}
        if(cdBar != null) cdBar.material.SetFloat("_Ratio", barAmount);

		if(Input.GetButton("ColorChange"))
		{
			Camo();
		}
        if (selector != null)
        {
            if (currentForm == 1)
            {
                selector.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(selector.GetComponent<RectTransform>().anchoredPosition, new Vector2(-130, 60), Time.deltaTime * 500);
            }
            else if (currentForm == -1)
            {
                selector.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(selector.GetComponent<RectTransform>().anchoredPosition, new Vector2(-70, 60), Time.deltaTime * 500);
            }
        }
	}
		
	IEnumerator Shrink()
	{
		transform.localScale -= (Vector3.one / 20);
		yield return new WaitForSeconds(Time.deltaTime);
		if(transform.localScale.x <= 0.2)
		{
			StartCoroutine("Grow");
			if(currentForm == 1)
			{
				GetComponent<MeshFilter>().sharedMesh = shape2.GetComponent<MeshFilter>().sharedMesh;
			}
			else if(currentForm == -1)
			{
				GetComponent<MeshFilter>().sharedMesh = shape1.GetComponent<MeshFilter>().sharedMesh;
			}
			currentForm *= -1;
		}
		else
		{
			StartCoroutine("Shrink");
		}
	}

	IEnumerator Grow()
	{
		transform.localScale += (Vector3.one / 20);
		yield return new WaitForSeconds(Time.deltaTime);
		if(transform.localScale.x <= 1)
		{
			StartCoroutine("Grow");
		}
		else
		{
			barCharging = true;
			yield return new WaitForSeconds(5);
			cd = true;
		}
	}
   
	void Camo()
	{
        if (Physics.Raycast(transform.position, -Vector3.up, out ground))
		{
            GetComponent<Renderer>().material.color = Color.red;// Color.Lerp(GetComponent<Renderer>().material.color, ground.transform.GetComponent<Renderer>().material.color, Time.deltaTime);
        }
       
	}
}