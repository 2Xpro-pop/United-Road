using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Edge: MonoBehaviour
{
    public static Edge selected;

    [SerializeField]
    private EdgeVm _edgeVm;

    public EdgeVm EdgeVm 
    { 
        get => _edgeVm; 
        set 
        {
            _edgeVm = value;
            RenderEdge();
        }
    }

    private void RenderEdge()
    {
        if (_edgeVm != null)
        {
            foreach(var box in GetComponents<BoxCollider>())
            {
                Destroy(box);
            }

            var lineRenderer = GetComponent<LineRenderer>();
            for(var i = 0; i < _edgeVm.points.Length; i++)
            {
                var point = _edgeVm.points[i];
                var vector = new Vector3(point.x, 0, point.y);
                lineRenderer.SetPosition(i, vector);
            }
            var lineMesh = new Mesh();
            lineRenderer.BakeMesh(lineMesh, true);
            lineRenderer.GetComponent<MeshCollider>().sharedMesh = lineMesh; //Set the baked mesh to the MeshCollider
            lineRenderer.GetComponent<MeshCollider>().convex = true;
        }
    }

    private void OnMouseDown()
    {
        selected = this;
    }

    private void Update()
    {
        var lineRenderer = GetComponent<LineRenderer>();
        if(selected == this)
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
        else
        {
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
    }


}
