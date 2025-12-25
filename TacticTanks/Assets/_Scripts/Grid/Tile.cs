using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int z;

    public TileType type;
    public bool walkable;

    private Renderer rend;
    private Material baseMaterial;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void Setup(int x, int z, TileType type, Material material, bool walkable)
    {
        this.x = x;
        this.z = z;
        this.type = type;
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
}
