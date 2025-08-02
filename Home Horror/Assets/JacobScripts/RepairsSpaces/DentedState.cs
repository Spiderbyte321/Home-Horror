using UnityEngine;

public class DentedState : ParentState
{
    private Mesh mesh;
    public override Mesh Mesh
    {
        get { return mesh; }
    }

    public DentedState(Mesh MeshToHold)
    {
        mesh = MeshToHold;
    }
}
