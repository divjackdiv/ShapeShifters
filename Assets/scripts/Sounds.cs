using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Sounds : NetworkBehaviour {
    
    public AudioSource GlobalSource;
    public AudioSource LocalSource;
    public AudioClip pointScored;
    public AudioClip flagStolen;
    public AudioClip shooting1;
    public AudioClip shooting2;
    public AudioClip shooting3;

    List<AudioClip> sounds;
    List<AudioClip> shootingSounds;
    // Use this for initialization
    void Start () {
        sounds = new List<AudioClip>();
        shootingSounds = new List<AudioClip>();
        if (flagStolen != null) sounds.Add(flagStolen);
        if (pointScored != null) sounds.Add(pointScored);
        if (shooting1 != null) shootingSounds.Add(shooting1);
        if (shooting2 != null) shootingSounds.Add(shooting2);
        if (shooting3 != null) shootingSounds.Add(shooting3);
    }
	
    [Command]
    public void CmdPlaySound(int i)
    {
        GlobalSource.PlayOneShot(sounds[i]);
        RpcPlaySound(i);
    }

    [Command]
    public void CmdPlayShootingSound(int i, float volume)
    {
        print(" WOOO CMDPLAY");
        LocalSource.PlayOneShot(shootingSounds[i], volume);
        RpcPlayShootingSound(i,volume);
    }

    [ClientRpc]
    void RpcPlaySound(int i)
    {
        GlobalSource.PlayOneShot(sounds[i]);
    }
    [ClientRpc]
    void RpcPlayShootingSound(int i,float volume)
    {
        print(volume + " yo, this is it bro");
        LocalSource.PlayOneShot(shootingSounds[i], volume);
    }
}
