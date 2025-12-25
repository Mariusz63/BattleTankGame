using System.Collections.Generic;
using UnityEngine;

public static class AttackRangeCalculator
{
    public static List<Tile> GetAttackRange(Unit unit)
    {
        List<Tile> result = new();

        int range = unit.stats.attackRange;

        int startX = unit.x;
        int startZ = unit.z;

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                int targetX = startX + x;
                int targetZ = startZ + z;

                // Manhattan distance (jak w XCOM)
                if (Mathf.Abs(x) + Mathf.Abs(z) > range)
                    continue;

                Tile tile = GridManager.Instance.GetTile(targetX, targetZ);
                if (tile == null)
                    continue;

                result.Add(tile);
            }
        }

        return result;
    }
}
