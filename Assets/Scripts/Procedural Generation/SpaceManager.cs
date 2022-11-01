using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager instance;

    public Vector3 spaceSize;
    public LayerMask layerMask;
    public float maxDistance;

    private void Awake()
    {
        if(SpaceManager.instance == null)
        {
            SpaceManager.instance = this;
        }
        else if(SpaceManager.instance != this)
        {
            Destroy(this);
        }    
    }
}
