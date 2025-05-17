using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

[System.Serializable]
public class Chain
{
    [SerializeField, HideInInspector]
    private Vector2[] chain;

    [SerializeField, HideInInspector]
    public float[] radius;

    public Vector2 head { 
        get { return chain[0]; }
        set {
            chain[0] = value;
            MoveBodyToHead();
        }
    }

    public Chain(int nPoint, Vector2 origin, float r)
    {
        chain = new Vector2[nPoint];
        radius = new float[nPoint];

        radius[0] = r;

        float Sum = 0;
        for(int i = 0; i < nPoint; i++)
        {
            if (i > 0) radius[i] = radius[i - 1];
            chain[i] = origin + Sum * Vector2.right;

            Sum += radius[i];
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

    private void MoveBodyToHead()
    {
        for (int i = 1; i < chain.Length; i++)
        {
            Vector2 dir = chain[i] - chain[i - 1];
            chain[i] = chain[i - 1] + dir.normalized * radius[i - 1];
        }
    }

    public void ChangeRadius(int id, float r)
    {
        radius[id] = r;
        float Sum = 0;
        for (int i = 0; i < chain.Length; i++)
        {
            chain[i] = chain[0] + Sum * Vector2.right;
            Sum += radius[i];
        }
    }

    public Vector2 this[int i]
    {
        get { return chain[i]; }
    }

    public int Length { get { return chain.Length; } }

    public void Draw()
    {
        Gizmos.color = Color.yellow;
        for(int i = 0; i <  chain.Length; i++)
        {
            Gizmos.DrawWireSphere(chain[i], radius[i]);
        }
    }


}