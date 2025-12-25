using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public MoveRangeVisual moveVisual;

    private Unit selectedUnit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        // 1️⃣ Kliknięcie w jednostkę
        if (hit.collider.TryGetComponent(out Unit unit))
        {
            SelectUnit(unit);
            return;
        }

        // 2️⃣ Kliknięcie w pole
        if (hit.collider.TryGetComponent(out Tile tile))
        {
            TryMoveToTile(tile);
        }
    }

    void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
        moveVisual.Show(unit);
    }

    void TryMoveToTile(Tile tile)
    {
        if (selectedUnit == null)
            return;

        if (!moveVisual.IsInRange(tile))
            return;

        selectedUnit.SetGridPosition(tile.x, tile.z);

        moveVisual.Clear();
        selectedUnit = null;
    }
}
