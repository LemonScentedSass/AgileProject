using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveLightning : MonoBehaviour
{
    public int LightningLevel = 0;


    public float SnapRadius = 1.00f;
    public Material Lightning;
    private Transform snapTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SphereCollider>().radius = SnapRadius;
        if (snapTarget != null)
        {
            transform.LookAt(snapTarget.transform.position);
        }
        if(LightningLevel == 0)
        {
            SnapRadius = 1f;
            if(GetComponent<ProjectileMotion>() == true)
            {
                GetComponent<ProjectileMotion>().speed = 10;
            }

        }
        if(LightningLevel == 1)
        {
            SnapRadius = 1.1f;
            if (GetComponent<ProjectileMotion>() == true)
            {
                GetComponent<ProjectileMotion>().speed = 15;
            }
        }
        if (LightningLevel == 2)
        {
            SnapRadius = 1.2f;
            if (GetComponent<ProjectileMotion>() == true)
            {
                GetComponent<ProjectileMotion>().speed = 20;
            }
        }
        if(LightningLevel == 3)
        {
            SnapRadius = 1.3f;
            if (GetComponent<ProjectileMotion>() == true)
            {
                GetComponent<ProjectileMotion>().speed = 25;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.AddComponent<MSTLightning>();
            collision.gameObject.GetComponent<MSTLightning>().LightningLVL = LightningLevel;
            collision.gameObject.GetComponent<MSTLightning>().material = Lightning;
            snapTarget = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && snapTarget == null)
        {
            snapTarget = other.transform;
        }
    }
}
