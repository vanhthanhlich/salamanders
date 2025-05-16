using UnityEngine;

public class ChainCreator : MonoBehaviour
{
    public Chain chain;

    public void CreateChain(int nPoint, int radius)
    {
        chain = new Chain(nPoint, transform.position, radius);
    }

}
