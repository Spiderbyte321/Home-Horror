using UnityEngine;

public class BrokenState : ParentState
{
    private Mesh mesh;
    
    private int moneyCost =0;

    private int materialCost = 0;
    public override Mesh Mesh => mesh;
    public override int MoneyCost => moneyCost;
    public override int MaterialCost => materialCost;

    public BrokenState(Mesh MeshToHold)
    {
        mesh = MeshToHold;
    }
}
