using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ColorChange : NetworkBehaviour
{
    RaycastHit ground;

    [SyncVar]
    private Color myColor;

    public override void OnStartClient()
    {
        GetComponent<Renderer>().material.color = myColor;
    }
    public void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetButton("ColorChange"))
        {
            changeColor();
        }
    }

    void changeColor()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out ground))
        {
            myColor = Color.Lerp(GetComponent<Renderer>().material.color, ground.transform.GetComponent<Renderer>().material.color, Time.deltaTime);
        }
        CmdOnColor(myColor);
    }

    //send color to server
    [Command]
    void CmdOnColor(Color col)
    {
        GetComponent<Renderer>().material.color = col;
        RpcColorChange(col);
    }

    //send color from server to Client
    [ClientRpc]
    void RpcColorChange(Color col)
    {
        GetComponent<Renderer>().material.color = col;
    }
}