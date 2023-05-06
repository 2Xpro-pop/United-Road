using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Prerender: MonoBehaviour
{
    public string json;
    public GameObject edgePrefab;
    public EdgeVm[] edges;

    private void OnValidate()
    {
        edges = JsonUtility.FromJson<MapVm>(json).edges;

        foreach(var child in transform.GetComponentsInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
       
        foreach (var edge in edges)
        {
            var edgeObject = Instantiate(edgePrefab);
            var edgeComponent = edgeObject.GetComponent<Edge>();
            edgeComponent.EdgeVm = edge;
            edgeObject.transform.parent = transform;
        }
    }
}