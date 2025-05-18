using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ChainCreator : MonoBehaviour
{
    [SerializeField]
    public Chain chain;
    public float moveSpeed;

    [SerializeField, Range(1, 30)] public int nPoint;
    [SerializeField] private float radius;

    private void Update()
    {
        CreateMesh();
    }

    public void CreateChain()
    {
        chain = new Chain(nPoint, transform.position, radius);
    }

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
    private void CreateMesh()
    {
        Mesh mesh = new Mesh();
        vertices.Clear();
        triangles.Clear();

        for(int i = 0; i < chain.Length; i++)
        {
            vertices.Add(chain[i].position - chain[i].norm);
            vertices.Add(chain[i].position + chain[i].norm);
        }

        int id = 0;
        for(int i = 0; i < chain.Length - 1; i ++)
        {
            triangles.Add(id + 1);
            triangles.Add(id);
            triangles.Add(id + 2);

            triangles.Add(id + 1);
            triangles.Add(id + 2);
            triangles.Add(id + 3);

            id += 2;
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (chain == null) return;
        
        foreach(var pos in vertices)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(pos, Vector3.one * 0.1f);
        }

        for(int i = 0; i < triangles.Count; i += 3)
        {
            Gizmos.DrawLine(vertices[triangles[i]], vertices[triangles[i + 1]]);
            Gizmos.DrawLine(vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
            Gizmos.DrawLine(vertices[triangles[i + 2]], vertices[triangles[i]]);
        }

        //chain.Draw();
    }

}
