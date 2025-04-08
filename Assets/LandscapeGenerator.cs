using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LandscapeGenerator : MonoBehaviour
{

    UnityEngine.Vector3[] vertices;
    int[] triangles;
    UnityEngine.Vector2[] uvs;
    Mesh mesh;
    public int xSize;
    public int zSize;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
       vertices = new UnityEngine.Vector3[(xSize + 1) * (zSize + 1)];

       for (int i = 0, z = 0; z <= zSize; z++)
       {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * 3f;
                vertices[i] = new UnityEngine.Vector3(x,y,z);
                i++;
            }
       }

       triangles = new int[xSize * zSize * 6];
       int vert = 0;
       int tris = 0;

       for (int z = 0; z < zSize; z++)
       {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
       }

       uvs = new UnityEngine.Vector2[vertices.Length];
       for (int i = 0, z = 0; z <= zSize; z++)
       {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new UnityEngine.Vector2((float)x, (float)z);
                i++;
            }
       }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uvs;
    }
    /*
    void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    */
}
