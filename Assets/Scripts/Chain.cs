using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Chain
{
    [Serializable]
    public struct Node
    {
        public Vector2 position;
        public Vector2 direction;
        public float radius;

        public Node(Vector2 position, float radius)
        {
            this.position = position;
            this.radius = radius;
            this.direction = Vector2.left;
        }

        public Vector2 norm { get { return radius * new Vector2(-direction.y, direction.x).normalized; } }

    }

    [SerializeField]
    public Node[] chain;

    public Vector2 head { 
        get { return chain[0].position; }
        set {
            chain[0].direction = ((Vector2)value - chain[0].position).normalized;
            chain[0].position = value;
            UpdateBody();
        }
    }

    public Chain(int nPoint, Vector2 origin, float r)
    {
        chain = new Node[nPoint];
        chain[0] = new Node(origin, r);

        for (int i = 1; i < nPoint; i++)
        {
            chain[i] = new Node()
            {
                radius = (1 - 0.1f) * chain[i - 1].radius,
                position = chain[i - 1].position + Vector2.right,
                direction = Vector2.left
            };
        }
    }

    public IEnumerator Move(Vector2 target, float moveSpeed)
    {
        float t = 0;
        float rate = moveSpeed / Vector2.Distance(head, target);

        while(t < 1) {
            t += Time.deltaTime * rate;
            head = Vector2.Lerp(head, target, t);
            
            yield return null;
        }
    }

    private void UpdateBody()
    {
        for (int i = 1; i < chain.Length; i++)
        {
            Vector2 dir = (chain[i].position - chain[i - 1].position).normalized;
            chain[i].position = chain[i - 1].position + dir;
            chain[i].direction = -dir;
        }
    }

    public void ChangeRadius(int id, float r)
    {
        chain[id].radius = r;
    }

    public Node this[int i]
    {
        get { return chain[i]; }
    }

    public int Length { get { return chain.Length; } }

    public void Draw()
    {

        void DrawOutLine()
        {

            for (int i = 1; i < chain.Length; i++)
            {
                Gizmos.DrawLine(chain[i - 1].position + chain[i - 1].norm, chain[i].position + chain[i].norm);
                Gizmos.DrawLine(chain[i - 1].position - chain[i - 1].norm, chain[i].position - chain[i].norm);
            }

            int id = 0;

            Gizmos.DrawLine(chain[id].position + chain[id].direction * chain[id].radius, chain[id].position + chain[id].norm);
            Gizmos.DrawLine(chain[id].position + chain[id].direction * chain[id].radius, chain[id].position - chain[id].norm);

            id = chain.Length - 1;

            Gizmos.DrawLine(chain[id].position - chain[id].direction * chain[id].radius, chain[id].position + chain[id].norm);
            Gizmos.DrawLine(chain[id].position - chain[id].direction * chain[id].radius, chain[id].position - chain[id].norm);
        }

        void DrawCircle()
        {
            for (int i = 0; i < chain.Length; i++)
            {
                if (i != 1) continue;
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(chain[i].position, chain[i].radius);
            }
        }

        DrawOutLine();
        // DrawCircle();
        
    }


}