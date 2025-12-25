using UnityEngine;

public class GridVisual : MonoBehaviour
{
    public void Highlight(Tile tile)
    {
        tile.GetComponent<Renderer>().material.color = Color.green;
    }
}
