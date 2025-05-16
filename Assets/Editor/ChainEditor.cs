using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(ChainCreator))]
public class ChainEditor : Editor
{
    ChainCreator creator;
    Chain chain { get { return creator.chain; } }
    private void OnEnable()
    {
        creator = (ChainCreator)target;
        creator.CreateChain(5, 1);
    }

    private void OnSceneGUI()
    {
        Draw();
    }

    private void Draw()
    {
        for(int i = 0; i < chain.Length; i++)
        {
            Vector2 new_pos = Handles.FreeMoveHandle(chain[i], 0.1f, Vector2.zero, Handles.CircleHandleCap);
            if(new_pos != chain[i])
            {
                chain.MovePoint(i, new_pos);
            } 
        }
    }

}
