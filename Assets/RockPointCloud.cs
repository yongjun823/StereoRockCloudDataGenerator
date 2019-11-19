using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pcx;

public class RockPointCloud : MonoBehaviour
{
    void OnDisable()
    {
 
    }

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        Debug.Log(vertices.Length);
    }
}
