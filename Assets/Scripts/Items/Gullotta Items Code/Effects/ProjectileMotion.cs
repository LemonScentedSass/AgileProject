using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public float speed;
    public float comebackTime;
    public bool comesBack;
    public Transform user;
    public Transform objectPool;

    // Update is called once per frame
    void Update()
    {
        if(comesBack == true)
        {
            comebackTime -= Time.deltaTime;

            if(comebackTime <= 0f)
            {
                transform.LookAt(user.position + new Vector3(0,1,0));
            }
        }

        transform.position += (transform.forward * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ayo whatht heall boy");
            transform.position = objectPool.position;
            gameObject.SetActive(false);
        }

        if(collision.gameObject)
        {
            comebackTime = 0;
        }
    }

}
