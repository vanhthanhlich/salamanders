using Unity.VisualScripting;
using UnityEngine;

public class ChainCreator : MonoBehaviour
{
    [SerializeField]
    public Chain chain;
    public float moveSpeed;

    [SerializeField, Range(1, 15)] public int nPoint;
    [SerializeField] private float radius;

    public void CreateChain()
    {
        chain = new Chain(nPoint, transform.position, radius);
    }

    private void Awake()
    {
        if(chain == null) {
            Debug.Log("cai deo gi vay");
            CreateChain();
        }
    }

    private void OnDrawGizmos()
    {
        if (chain == null) return;
        chain.Draw();
    }

}
