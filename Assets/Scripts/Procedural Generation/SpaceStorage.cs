using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStorage : MonoBehaviour
{
    [SerializeField] private List<Transform> _myTransforms = new List<Transform>();

    private void Awake()
    {
        if (SpaceManager.instance == null)
        {
            return;
        }

        //GetComponent<BoxCollider>().isTrigger = true;

        Collider[] hits = Physics.OverlapBox(transform.position, SpaceManager.instance.spaceSize);

        for (int i = 0; i < hits.Length; i++)
        {
            if (_myTransforms.Contains(hits[i].transform) == false)
            {
                _myTransforms.Add(hits[i].transform);
            }
        }

    }

    public void Visulize(bool flag)
    {
        for (int i = 0; i < _myTransforms.Count; i++)
        {
            _myTransforms[i].gameObject.SetActive(flag);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, SpaceManager.instance.spaceSize);
    }
}
