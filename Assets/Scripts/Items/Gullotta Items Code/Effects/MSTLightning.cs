using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MSTLightning : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private LineRenderer _lineRenderer;

    private List<Collider> _hits = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Cast();
    }

    private void Update()
    {
        if(_lineRenderer == null || _hits == null)
        {
            return;
        }

        Debug.Log("Hits: " + _hits.Count);
        List<Vector3> points = new List<Vector3>();

        for(int i = 0; i < _hits.Count; i++)
        {
            points.Add(_hits[i].transform.position);
        }


        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }

    void Cast()
    {
        List<Collider> unsorted = new List<Collider>(Physics.OverlapSphere(transform.position, radius, layerMask));
        List<Collider> sorted = new List<Collider>();

        if(GetComponent<Collider>() != null)
        {
            unsorted.Insert(0, GetComponent<Collider>());
        }

        sorted.Add(unsorted[0]);
        unsorted.RemoveAt(0);

        while(unsorted.Count > 0)
        {
            float curBestDistance = Mathf.Infinity;

            int unsortedIndex = 0;

            for(int i = 0; i < sorted.Count; i++)
            {
                for (int j = 0; j < unsorted.Count; j++)
                {

                    float dist = Vector3.Distance(sorted[i].transform.position, unsorted[j].transform.position);

                    if (dist < curBestDistance)
                    {
                        curBestDistance = dist;
                        unsortedIndex = j;
                    }

                }
            }

            sorted.Add(unsorted[unsortedIndex]);
            unsorted.Remove(unsorted[unsortedIndex]);
        }


        Debug.Log(sorted.Count);
        _hits = sorted;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
