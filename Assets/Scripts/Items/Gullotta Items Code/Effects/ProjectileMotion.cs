using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{

    public Transform user;
    public Transform attackDirection;

    public Transform objectPool;
    public GameObject ExplosionPrefab;
    public float explosionTime;
    public bool travelTime;
    public float TravelTime;
    public bool explodes;
    public float explodeRadius;
    public float speed;
    public float comebackTime;
    public bool comesBack;
    private float timeTracker;
    private float timeTracker2;
    private bool Hit;

    private bool Exploded = false;

    private GameObject fireballExplosion;

    private Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);

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

        if(travelTime == true && explodes == false && CharacterTest.instance.Using == true)
        {
            TravelTime -= Time.deltaTime;
            //broken
            if (TravelTime <= 0f)
            {
                //do something
                transform.position = objectPool.position;
            }
        }

        if(Exploded != true)
        {
            transform.position += (transform.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position;
        }
     

        if (explodes)
        {
            Explodes();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.transform.localScale.magnitude);
        if(explodes)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Hit = true;
                Debug.Log("wtf, why two balls");
                fireballExplosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                transform.position = objectPool.position;
                Exploded = true;

            }
        }

        if(comesBack == true)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                transform.position = objectPool.position;
                gameObject.SetActive(false);
            }

            if (collision.gameObject)
            {
                comebackTime = 0;
            }
        }
        else if(comesBack == false && explodes == false)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                transform.position = objectPool.position;
                CharacterTest.instance.Using = false;
                gameObject.SetActive(false);
            }

        }

    }

    public void Explodes()
    {
        //explosion prefab time
        if (Exploded == true)
        {
           
            if (timeTracker2 >= explosionTime)
            {
                Destroy(fireballExplosion);
                timeTracker2 -= explosionTime;
                Hit = false;
                Exploded = false;
                CharacterTest.instance.Using = false;
                gameObject.SetActive(false);
            }

            if(Exploded == true)
            {
                timeTracker2 += Time.deltaTime;
            }
            else
            {
                timeTracker2 = 0;
            }


        }
        //explosion after travel time
        if(Exploded != true && travelTime == true && Hit == false)
        {
            Debug.Log("alsdjfl;asjdfl;asjdf;lajsd;ljasl;dfja");
            if(timeTracker >= TravelTime)
            {
                Debug.Log("explosion");
                fireballExplosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                transform.position = objectPool.position;
                Exploded = true;
            }

            if(Exploded != true)
            {
                timeTracker += Time.deltaTime;
            }
            else
            {
                timeTracker = 0;
            }
           
        }
       
       
        if (fireballExplosion != null)
        {
            if (fireballExplosion.transform.localScale.magnitude < explodeRadius)
            {
                fireballExplosion.transform.localScale += scale;
                Debug.Log(fireballExplosion.transform.localScale.magnitude);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, explodeRadius);
    }

}