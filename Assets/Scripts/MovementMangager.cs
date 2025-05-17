using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class MovementMangager : MonoBehaviour
{
    ChainCreator creator;
    Chain chain { get { return creator.chain; } }

    private Coroutine coroutine;

    void Awake()
    {
        creator = GetComponent<ChainCreator>();
    }

    public void UpdateMovement(Vector2 target)
    {
        if(creator == null) return;
        
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(chain.Move(target));
    }

    private void Update()
    {
        // Debug.Log("huhu");
        Inp();
    }

    private void Inp()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            UpdateMovement(mousePos);
        }
    }

}
