using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : ProjectileMotion
{

    [SerializeField, Range(0.1f, 100f)] float _pullSpeed = 1f;
    /*
    public GameObject player; 
    public Transform startPoint;
    public Transform endPoint;
    */
    //[SerializeField] public float speed = 1.0f;

    //private float startTime;
    //private float travelLength;

    /*
    private void Start()
    {
        startPoint = _source.transform;
        startTime = Time.deltaTime;
        travelLength = Vector3.Distance(startPoint.position, endPoint.position);
    }
    */

    private bool _flag = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_flag == false)
        {
            StartCoroutine(HookshotPull(collision.collider));
        }
    }

    IEnumerator HookshotPull(Collider col)
    {
        _flag = true;

        GetComponent<Collider>().enabled = false;

        Vector3 startPos = user.position;
        Vector3 endPos = col.transform.position;

        Vector3 direction = endPos - startPos;

        float distance = direction.magnitude;
        float t = 0;
        float duration = distance/ _pullSpeed;

        speed = 0;

        direction.Normalize();

        while (t < duration)
        {
            t += Time.deltaTime;
            user.position = Vector3.Lerp(startPos, endPos - direction * 1.5f, t/duration);
            yield return null;

            Debug.Log(duration + "/" + t);
        }

        Destroy(gameObject);
    }
}