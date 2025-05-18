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
    List<Vector2> idkman = new List<Vector2>();    

    private void CreateMesh()
    {
        Mesh mesh = new Mesh();
        vertices.Clear();
        triangles.Clear();
        idkman.Clear();

        void AddTriangle(int i, int j, int k)
        {
            triangles.Add(i);
            triangles.Add(j);
            triangles.Add(k);
        }

        for(int i = 0; i < chain.Length; i++)
        {
            vertices.Add(chain[i].position - chain[i].norm * chain[i].radius);
            vertices.Add(chain[i].position + chain[i].norm * chain[i].radius);
        }

        int id = 0;
        for(int i = 0; i < chain.Length - 1; i ++)
        {
            AddTriangle(id + 1, id, id + 2);
            AddTriangle(id + 1, id + 2, id + 3);

            id += 2;
        }

        int x = 3;
        
        void FormTriangle(int id)
        {
            int l = vertices.Count;
            AddTriangle(l - x, id, id + 1);
            for (int i = 1; i < x; i++) AddTriangle(l - i, id, l - i - 1);
        }

        void go(int id, float angle)
        {
            Vector2 n = chain[id].norm;
            float a0 = Mathf.Atan2(n.y, n.x);
            
            for(int i = 1; i <= x; i ++)
            {
                float theta = i * angle + a0;
                Vector2 v = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * chain[id].radius;
                vertices.Add(chain[id].position + v);
            }

        }

        int curlast = vertices.Count - 2;

        go(0, -Mathf.PI / (x + 1));
        FormTriangle(0);

        go(chain.Length - 1, Mathf.PI / (x + 1));
        FormTriangle(curlast);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (chain == null) return;
        
        //foreach(var pos in idkman)
        //{
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawCube(pos, Vector3.one * 0.1f);
        //}

        //for (int i = 0; i < triangles.Count; i += 3)
        //{
        //    Gizmos.DrawLine(vertices[triangles[i]], vertices[triangles[i + 1]]);
        //    Gizmos.DrawLine(vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
        //    Gizmos.DrawLine(vertices[triangles[i + 2]], vertices[triangles[i]]);
        //}

        //chain.Draw();
    }

}
