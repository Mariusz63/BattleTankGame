using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int z;

    public TileType tileType;
    public bool walkable;

    private Renderer rend;
    private Material baseMaterial;

    [Header("Visuals")]
    public GameObject content; // np. budynek, drzewo


    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void Setup(int x, int z, TileType type, Material material, bool walkable)
    {
        this.x = x;
        this.z = z;
        this.tileType = type;
        this.walkable = walkable;

        baseMaterial = material;
        rend.material = baseMaterial;
    }

    public void Highlight(Material highlightMaterial)
    {
        rend.material = highlightMaterial;
    }

    public void ResetMaterial()
    {
        rend.material = baseMaterial;
    }

    public void SetTileType(TileType newType)
    {
        tileType = newType;

        switch (tileType)
        {
            case TileType.Grass:
                walkable = true;
                break;

            case TileType.Obstacle:
            case TileType.Building:
            case TileType.Water:
                walkable = false;
                break;
        }
    }

    public void ClearContent()
    {
        if (content != null)
        {
            Destroy(content);
            content = null;
        }
    }
}
