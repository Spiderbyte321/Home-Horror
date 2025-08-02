using UnityEngine;

public class RepairedState : ParentState
{
    private Mesh mesh;
    public override Mesh Mesh
    {
        get { return mesh; }
    }


    public RepairedState(Mesh MeshToHold)
    {
        mesh = MeshToHold;
    }
    
}
