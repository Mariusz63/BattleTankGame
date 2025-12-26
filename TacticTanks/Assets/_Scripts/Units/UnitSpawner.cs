using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Units to spawn")]
    public List<UnitSpawnInfo> unitsToSpawn;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance != null);
        yield return new WaitUntil(() => GridManager.Instance.grid != null);

        SpawnAll();
    }


    public void SpawnAll()
    {
        foreach (UnitSpawnInfo info in unitsToSpawn)
        {
            SpawnUnit(info);
        }
    }

    void SpawnUnit(UnitSpawnInfo info)
    {
        Tile tile = GridManager.Instance.GetTile(info.x, info.z);

        if (tile == null)
        {
            Debug.LogError($"Brak tile na ({info.x},{info.z})");
            return;
        }

        // JEŚLI TILE BLOKUJE – WYCZYŚĆ
        if (!tile.walkable)
        {
            Debug.LogWarning($"Pole ({info.x},{info.z}) zablokowane. Czyszczenie...");
            GridManager.Instance.ClearTileAndMakeGrass(info.x, info.z);
        }

        // Dodatkowe zabezpieczenie
        if (GridManager.Instance.IsTileOccupied(info.x, info.z))
        {
            Debug.LogWarning($"Pole ({info.x},{info.z}) zajęte przez inną jednostkę");
            return;
        }

        GameObject unitObj = Instantiate(
            info.unitPrefab,
            tile.transform.position + Vector3.up * 0.5f,
            Quaternion.identity
        );

        Unit unit = unitObj.GetComponent<Unit>();
        unit.SetGridPosition(info.x, info.z);
    }

}
