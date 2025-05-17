using System.Collections;
using UnityEngine;

public class Chain
{
    private Vector2[] chain;
    private float radius;

    public Vector2 head { 
        get { return chain[0]; }
        set {
            chain[0] = value;
            MoveBodyToHead();
        }
    }

    public Chain(int nPoint, Vector2 origin, float radius)
    {
        chain = new Vector2[nPoint];
        this.radius = radius;

        for (int i = 0; i < nPoint; i++)
        {
            chain[i] = origin + Vector2.right * radius * i;
        }
    }

    private float smooth(float t) {
        return 3 * t * t - 2 * t * t * t;
    }

    public IEnumerator Move(Vector2 target)
    {
        float t = 0;
        float rate = 20 / Vector2.Distance(head, target);
        while(t < 1) {
            t += rate * Time.deltaTime;
            head = Vector2.Lerp(head, target, t);
            
            yield return null;
        }
    }

    private void MoveBodyToHead()
    {
        for (int i = 1; i < chain.Length; i++)
        {
            Vector2 dir = chain[i] - chain[i - 1];
            chain[i] = chain[i - 1] + dir.normalized * radius;
        }
    }

    public Vector2 this[int i]
    {
        get { return chain[i]; }
    }

    public int Length { get { return chain.Length; } }

}