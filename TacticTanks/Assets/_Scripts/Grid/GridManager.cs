using UnityEngine;
using UnityEngine.Rendering;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;


    [Header("Grid Size")]
    public int width = 10;
    public int height = 10;

    [Header("Tile Prefab")]
    public GameObject tilePrefab;

    [Header("Materials")]
    public Material grassMat;
    public Material meadowMat;
    public Material waterMat;
    public Material obstacleMat;
    public Material buildingMat;

    [Header("Tile Heights")]
    public float groundHeight = 0.2f;
    public float obstacleHeight = 0.8f;
    public float buildingHeight = 1.2f;
    public float waterHeight = 0.1f;

    public Tile[,] grid;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                TileType type = GetRandomTileType();

                GameObject tileObj = Instantiate(
                    tilePrefab,
                    new Vector3(x, 0, z),
                    Quaternion.identity,
                    transform
                );

                Tile tile = tileObj.GetComponent<Tile>();

                ApplyTileVisuals(tileObj, tile, x, z, type);

                grid[x, z] = tile;
            }
        }
    }

    void ApplyTileVisuals(GameObject obj, Tile tile, int x, int z, TileType type)
    {
        Material mat = grassMat;
        float height = groundHeight;
        bool walkable = true;

        switch (type)
        {
            case TileType.Grass:
                mat = grassMat;
                break;

            case TileType.Meadow:
                mat = meadowMat;
                height = groundHeight;
                break;

            case TileType.Water:
                mat = waterMat;
                height = waterHeight;
                walkable = false;
                break;

            case TileType.Obstacle:
                mat = obstacleMat;
                height = obstacleHeight;
                walkable = false;
                break;

            case TileType.Building:
                mat = buildingMat;
                height = buildingHeight;
                walkable = false;
                break;
        }

        obj.transform.localScale = new Vector3(1, height, 1);
        obj.transform.position += Vector3.up * (height / 2f);

        tile.Setup(x, z, type, mat, walkable);
    }

    // Przyk³ad funkcji losuj¹cej typ
    TileType GetRandomTileType()
    {
        int rand = Random.Range(0, 100);
        if (rand < 4) return TileType.Obstacle;
        if (rand < 8) return TileType.Water;
        if (rand < 55) return TileType.Meadow;
        if (rand < 96) return TileType.Grass;       
        return TileType.Building;                    
    }


    public Tile GetTile(int x, int z)
    {
        if (grid == null)
        {
            Debug.LogError("Grid not initialized!");
            return null;
        }

        if (x < 0 || z < 0 || x >= width || z >= height)
            return null;

        return grid[x, z];
    }


    public bool IsTileOccupied(int x, int z)
    {
        foreach (Unit u in FindObjectsOfType<Unit>())
        {
            if (u.x == x && u.z == z)
                return true;
        }
        return false;
    }

    public void ClearTileAndMakeGrass(int x, int z)
    {
        Tile tile = GetTile(x, z);
        if (tile == null) return;

        tile.ClearContent();
        tile.SetTileType(TileType.Grass);
    }

}
