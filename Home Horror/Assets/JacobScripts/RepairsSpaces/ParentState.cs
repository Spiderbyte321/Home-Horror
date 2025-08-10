using UnityEngine;

public abstract class ParentState//parent of the whole state machine
{
    public virtual Mesh Mesh { get; }

    public virtual int MoneyCost { get; }

    public virtual int MaterialCost { get; }

    //make a way for states to progress to one another


}
