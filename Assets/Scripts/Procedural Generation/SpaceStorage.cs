using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStorage : MonoBehaviour
{
    [SerializeField] private Collider[] _myTransforms;

    private void Awake()
    {
        if (SpaceManager.instance == null)
        {
            return;
        }

        //GetComponent<BoxCollider>().isTrigger = true;

        _myTransforms = Physics.OverlapBox(transform.position, SpaceManager.instance.spaceSize);

    }

    public void Visulize(bool flag)
    {
        for (int i = 0; i < _myTransforms.Length; i++)
        {
            _myTransforms[i].gameObject.SetActive(flag);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, SpaceManager.instance.spaceSize);
    }
}
