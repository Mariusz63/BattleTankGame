using System.Collections.Generic;
using UnityEngine;

public class MoveRangeVisual : MonoBehaviour
{
    [Header("Materials")]
    public Material moveRangeMaterial;

    private List<Tile> highlightedTiles = new();

    public void Show(Unit unit)
    {
        Clear();

        highlightedTiles = MoveRangeCalculator.GetMoveRange(unit);

        foreach (Tile tile in highlightedTiles)
        {
            tile.Highlight(moveRangeMaterial);
        }
    }

    public void Clear()
    {
        foreach (Tile tile in highlightedTiles)
        {
            tile.ResetMaterial();
        }

        highlightedTiles.Clear();
    }

    public bool IsInRange(Tile tile)
    {
        return highlightedTiles.Contains(tile);
    }
}
