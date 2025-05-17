using Codice.CM.Common.Tree.Partial;
using Unity.Mathematics;
using UnityEditor;

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

    private void OnSceneGUI()
    {
        Draw();
    }

    private void Draw()
    {
        for(int i = 0; i < chain.Length; i ++)
        {
            float new_radius = Handles.RadiusHandle(quaternion.identity, chain[i], chain.radius[i] / 2);
            if(new_radius * 2 !=  chain.radius[i]) {
                Undo.RecordObject(creator, "change radius of point " + i);
                chain.ChangeRadius(i, new_radius * 2);
            }
        }
    }

}
