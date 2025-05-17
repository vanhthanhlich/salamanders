using Unity.VisualScripting;
using UnityEngine;

public class ChainCreator : MonoBehaviour
{
    public Chain chain;
    public float moveSpeed;
    [SerializeField] private int nPoint;
    [SerializeField] private float radius;

    public void CreateChain(int nPoint, float radius)
    {
        chain = new Chain(nPoint, transform.position, radius);
    }

    private void Awake()
    {
        CreateChain(nPoint, radius);
    }

    private void OnDrawGizmos()
    {
        if (chain == null) return;
        chain.Draw();
    }

}
