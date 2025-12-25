using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public PlayerActionMode currentMode = PlayerActionMode.None;

    public Unit selectedUnit;

    public MoveRangeVisual moveRangeVisual;
    public AttackRangeVisual attackRangeVisual;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleClick();
    }

    public void SelectUnit(Unit unit)
    {
        ClearSelection();
        selectedUnit = unit;
    }

    public void SetMoveMode()
    {
        currentMode = PlayerActionMode.Move;
        moveRangeVisual.Show(selectedUnit);
    }

    public void SetAttackMode()
    {
        currentMode = PlayerActionMode.Attack;
        attackRangeVisual.Show(selectedUnit);
    }

    public void ClearSelection()
    {
        moveRangeVisual.Clear();
        attackRangeVisual.Clear();
        selectedUnit = null;
        currentMode = PlayerActionMode.None;
    }

    void HandleClick()
    {
        Console.WriteLine("HandleClick called");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        // Klik na TANKA
        if (hit.collider.CompareTag("Tank"))
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                SelectUnit(unit);
                return;
            }
        }

        // Klik na Tile
        Tile tile = hit.collider.GetComponent<Tile>();
        if (tile != null)
        {
            HandleTileClick(tile);
            return;
        }

        // Klik na wroga
        if (hit.collider.CompareTag("Enemy"))
        {
            Unit enemy = hit.collider.GetComponent<Unit>();
            if (enemy != null)
            {
                HandleEnemyClick(enemy);
            }
        }
    }


    void HandleTileClick(Tile tile)
    {
        if (selectedUnit == null)
            return;

        if (currentMode == PlayerActionMode.Move &&
            moveRangeVisual.IsInRange(tile))
        {
            selectedUnit.SetGridPosition(tile.x, tile.z);
            ClearSelection();
        }
    }

    void HandleEnemyClick(Unit enemy)
    {
        if (selectedUnit == null)
            return;

        if (currentMode == PlayerActionMode.Attack &&
            attackRangeVisual.IsInRange(enemy))
        {
            enemy.TakeDamage(selectedUnit.stats.damage);
            ClearSelection();
        }
    }
}
