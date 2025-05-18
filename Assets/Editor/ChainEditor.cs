using Codice.CM.Common.Tree.Partial;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChainCreator))]
public class ChainEditor : Editor
{
    ChainCreator creator;
    Chain chain { get { return creator.chain; } }

    private void OnEnable()
    {
        creator = (ChainCreator)target;
        if(chain == null) creator.CreateChain();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("reset"))
        {
            Undo.RecordObject(creator, "reset");
            creator.CreateChain();
        }

    }

    private void OnSceneGUI()
    {
        Draw();
    }

    private void Draw()
    {
        for(int i = 0; i < chain.Length; i ++)
        {
            float new_radius = Handles.RadiusHandle(quaternion.identity, chain[i].position, chain[i].radius);
            if(new_radius !=  chain[i].radius) {
                Undo.RecordObject(creator, "change radius of point " + i);
                chain.ChangeRadius(i, new_radius);
            }
        }
    }

}
