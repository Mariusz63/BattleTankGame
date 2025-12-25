using System.Collections.Generic;

public static class MoveRangeCalculator
{
    public static List<Tile> GetMoveRange(Unit unit)
    {
        Tile start = GridManager.Instance.GetTile(unit.x, unit.z);

        Queue<Tile> queue = new();
        Dictionary<Tile, int> cost = new();
        List<Tile> result = new();

        queue.Enqueue(start);
        cost[start] = 0;

        while (queue.Count > 0)
        {
            Tile current = queue.Dequeue();

            foreach (Tile next in GetNeighbours(current))
            {
                if (!next.walkable)
                    continue;

                int newCost = cost[current] + 1;

                if (newCost > unit.stats.movementRange)
                    continue;

                if (!cost.ContainsKey(next))
                {
                    cost[next] = newCost;
                    queue.Enqueue(next);
                    result.Add(next);
                }
            }
        }

        return result;
    }

    static List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> list = new();

        int x = tile.x;
        int z = tile.z;

        TryAdd(list, x + 1, z);
        TryAdd(list, x - 1, z);
        TryAdd(list, x, z + 1);
        TryAdd(list, x, z - 1);

        return list;
    }

    static void TryAdd(List<Tile> list, int x, int z)
    {
        Tile tile = GridManager.Instance.GetTile(x, z);
        if (tile != null)
            list.Add(tile);
    }
}
