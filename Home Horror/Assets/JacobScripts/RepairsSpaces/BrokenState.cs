using UnityEngine;

public class BrokenState : ParentState
{
    private Mesh mesh;
    public override Mesh Mesh
    {
        get { return mesh; }
    }

    public BrokenState(Mesh MeshToHold)
    {
        mesh = MeshToHold;
    }
}
