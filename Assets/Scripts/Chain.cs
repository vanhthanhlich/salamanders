using UnityEngine;

public class Chain
{
    private Vector2[] chain;
    private float radius;

    public Chain(int nPoint, Vector2 origin, float radius)
    {
        chain = new Vector2[nPoint];
        this.radius = radius;

        for (int i = 0; i < nPoint; i++)
        {
            chain[i] = origin + Vector2.right * radius * i;
        }
    }

    public void MovePoint(int id, Vector2 pos)
    {
        if (id != 0) return;

        chain[0] = pos;
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