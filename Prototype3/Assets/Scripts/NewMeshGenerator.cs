using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMeshGenerator : MonoBehaviour
{
    // The material of the plane
    public Material material;

    // The Size of the plane X and Z axis
    public float x;
    public float z;

    // The Number of the x subdivisions for the x axis
    public int xBranches;
    public int zBranches;

    // The mesh renderer
    private MeshRenderer mr;

    // The mesh filter
    private MeshFilter mf;

    // The mesh collider
    private MeshCollider mc;


    public void SpawnMeshes()
    {
        // Initial the meshes
        Mesh m = new Mesh();
        mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = m;

        mc = gameObject.AddComponent<MeshCollider>();
        mr = gameObject.AddComponent<MeshRenderer>();
        mr.material = material;

        //creat vertice
        Vector3[] vertice = new Vector3[(xBranches + 1) * (zBranches + 1)];

        //Initial uv
        Vector2[] uv = new Vector2[vertice.Length];

        float xbranchesLength = x / (float)xBranches;
        float zbranchesLength = z / (float)zBranches;

        for (int i = 0, zAxis = 0; zAxis < zBranches+1;zAxis++ )
        {

            for (int xAxis = 0; xAxis < zBranches + 1; xAxis++,i++)
            {
                vertice[i] = new Vector3(xAxis * xbranchesLength, 0, zAxis * zbranchesLength);
                uv[i] = new Vector2((float)xAxis / xBranches, (float)zAxis / zBranches);
            }

        }

        m.vertices = vertice;
        m.uv = uv;

        int[] triangles = new int[xBranches * zBranches * 6];

        for (int ti = 0, vi = 0, y = 0; y < zBranches; y++, vi++)
        {
            for (int x = 0; x < xBranches; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xBranches + 1;
                triangles[ti + 5] = vi + xBranches + 2;
            }
        }
        m.triangles = triangles;
        m.RecalculateNormals();


    }

    private void Awake()
    {
        SpawnMeshes();
    }


}
