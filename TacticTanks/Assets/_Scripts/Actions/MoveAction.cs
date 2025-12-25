using UnityEngine;

public class MoveAction : BaseAction
{
    public Unit unit;
    public int targetX;
    public int targetZ;

    public override void Execute()
    {
        if (unit == null)
            return;

        unit.SetGridPosition(targetX, targetZ);
    }
}
