using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpaceStorage : MonoBehaviour
{
    [SerializeField]private Transform[] _myTransforms;
    public SpaceStorage[] neighbours;

    private void Awake()
    {
        if(SpaceManager.instance == null)
        {
            return;
        }

        GetComponent<BoxCollider>().isTrigger = true;

        RaycastHit[] hits = Physics.BoxCastAll(transform.position, SpaceManager.instance.spaceSize, transform.up, Quaternion.identity, SpaceManager.instance.maxDistance, SpaceManager.instance.layerMask);

        Debug.Log(hits.Length);

        _myTransforms = new Transform[hits.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            _myTransforms[i] = hits[i].transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
