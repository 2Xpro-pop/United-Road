using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    public GameObject edgePrefab;

    private EdgeVm[] _edges;

    void Start()
    {
        _edges = JsonUtility.FromJson<MapVm>(Resources.Load<TextAsset>("map").text).edges;
        foreach (var edge in _edges)
        {
            var edgeObject = Instantiate(edgePrefab);
            var edgeComponent = edgeObject.GetComponent<Edge>();
            edgeComponent.EdgeVm = edge;
            edgeObject.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class MapVm
{
    public EdgeVm[] edges;
}