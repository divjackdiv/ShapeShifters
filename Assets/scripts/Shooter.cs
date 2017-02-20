using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Shooter : NetworkBehaviour
{
	public GameObject RedBulletPrefab;
    public GameObject BlueBulletPrefab;
    public GameObject cam;
    float reloadTime = 2;
	float t = 2;
    GameObject shot;

    //Sound Management
    AudioSource source;

    void Update () 
	{
        if (!isLocalPlayer) return;
        if (cam == null) cam = GetComponent<PlayerControl>().cameraPointer;
        t += Time.deltaTime;
        if (Input.GetButton("shoot") && t >= reloadTime  && GetComponent<PlayerControl>().IsAiming())
        {
            t = 0;
            CmdOnShoot(cam.transform.rotation.x, cam.transform.rotation.y, cam.transform.rotation.z, cam.transform.rotation.w, GetComponent<Player>().team);
            GetComponent<Sounds>().CmdPlayShootingSound(0, 0.8f);
        }
    }

    [Command]
    void CmdOnShoot(float x, float y, float z, float w, int team)
    {
        int random = Random.Range(0, 2);
        if(team == 1) shot = Instantiate(RedBulletPrefab);
        else shot = Instantiate(BlueBulletPrefab);
        Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
        shot.GetComponent<Bullet>().team = team;
        shot.transform.rotation = new Quaternion(x,y,z,w);
        shot.transform.position = transform.position + shot.transform.forward;
        shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * 60;
        NetworkServer.Spawn(shot);
    }

}