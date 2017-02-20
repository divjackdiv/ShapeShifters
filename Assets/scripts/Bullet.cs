using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    float t = 0;
    public int team;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        t += Time.deltaTime;
        if (t > 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            if (coll.GetComponent<Player>().team != team)
            {
                coll.transform.position = coll.GetComponent<Player>().spawnPoint;
            }
        }
        Destroy(gameObject);
    }
    
}
