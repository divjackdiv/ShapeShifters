using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class FormChange : NetworkBehaviour
{


    [SyncVar]
    private int index = 0;
    public List<GameObject> shapes;
    public GameObject shape1;
    public GameObject shape2;
    public GameObject shape3;

    private List<Mesh> forms = new List<Mesh>();
    int currentForm = 1;
    [SyncVar]
    int s1;
    [SyncVar]
    int s2;

    //Stuff to do with hud
    public Image selector;
    public Image cdBar;
    bool cd = true;
    bool barCharging = false;
    float barAmount = 1;
    float t = 2;

    public override void OnStartClient()
    {
        forms.Add(shape1.GetComponent<MeshFilter>().sharedMesh);
        forms.Add(shape2.GetComponent<MeshFilter>().sharedMesh);
        forms.Add(shape3.GetComponent<MeshFilter>().sharedMesh);
        GetComponent<MeshFilter>().sharedMesh = forms[s1];
    }
    void Start()
    {
        forms.Add(shape1.GetComponent<MeshFilter>().sharedMesh);
        forms.Add(shape2.GetComponent<MeshFilter>().sharedMesh);
        forms.Add(shape3.GetComponent<MeshFilter>().sharedMesh);
        GetComponent<MeshFilter>().sharedMesh = forms[s1];

        int rN = Random.Range(0, 2);
        print("Rn =  " + rN);
        switch (rN)
        {
            case 0:
                s1 = 0;
                s2 = 1;
                break;
            case 1:
                s1 = 0;
                s2 = 2;
                break;
            case 2:
                s1 = 1;
                s2 = 2;
                break;
        }
    }

    public void Update()
    {
        t += Time.deltaTime;
        if (!isLocalPlayer) return;

        if (barCharging == true)
        {
            barAmount += (Time.deltaTime / 5);
            if (barAmount > 1)
            {
                barCharging = false;
            }
        }

        if (Input.GetButton("FormChange") && cd == true)
        {
            cd = false;
            barAmount = 0;
            StartCoroutine("Shrink");
        }
        if (cdBar != null) cdBar.material.SetFloat("_Ratio", barAmount);

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

    [Command]
    void CmdOnForm(int i)
    {
        GetComponent<MeshFilter>().sharedMesh = forms[i];
        RpcFormChange(i);
    }

    [ClientRpc]
    void RpcFormChange(int i)
    {
        GetComponent<MeshFilter>().sharedMesh = forms[i];
    }


    // NEXT COMES THE GROW AND SHRINK FUNCS 
    IEnumerator Shrink()
    {
        transform.localScale -= (Vector3.one / 20);
        yield return new WaitForSeconds(Time.deltaTime);
        if (transform.localScale.x <= 0.2)
        {
            StartCoroutine("Grow");
            if (index == s1)
            {
                index = s2;
            }
            else
            {
                index = s1;
            }
            print(index);
            CmdOnForm(index);
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
        if (transform.localScale.x <= 1)
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
}