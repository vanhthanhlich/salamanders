using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(ChainCreator))]
public class ChainEditor : Editor
{
    ChainCreator creator;
    Chain chain { get { return creator.chain; } }

    MovementMangager movementMangager;

    private void OnEnable()
    {
        creator = (ChainCreator)target;
        creator.CreateChain(10, 1);

        movementMangager = target.GetComponent<MovementMangager>();
    }

    private void OnSceneGUI()
    {
        Inp();
        Draw();
    }

    private void Inp()
    {
        Event guiEvent = Event.current;

            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            // if(movementMangager == null) Debug.Log("wtf");
            movementMangager.UpdateMovement(mousePos);
        
    }

    private void Draw()
    {
        for(int i = 0; i < chain.Length; i++)
        {
            // Vector2 new_pos = Handles.FreeMoveHandle(chain[i], 0.1f, Vector2.zero, Handles.CircleHandleCap);
            Handles.DrawWireDisc(chain[i], Vector3.forward, 0.5f);
        }
    }

}
