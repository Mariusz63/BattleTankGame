using System.Collections.Generic;
using UnityEngine;

public class AttackRangeVisual : MonoBehaviour
{
    public Material attackRangeMaterial;

    private List<Tile> tilesInRange = new();

    public void Show(Unit unit)
    {
        Clear();

        tilesInRange = AttackRangeCalculator.GetAttackRange(unit);

        foreach (Tile tile in tilesInRange)
            tile.Highlight(attackRangeMaterial);
    }

    public void Clear()
    {
        foreach (Tile tile in tilesInRange)
            tile.ResetMaterial();

        tilesInRange.Clear();
    }

    public bool IsInRange(Unit enemy)
    {
        foreach (Tile tile in tilesInRange)
        {
            if (enemy.x == tile.x && enemy.z == tile.z)
                return true;
        }
        return false;
    }
}
