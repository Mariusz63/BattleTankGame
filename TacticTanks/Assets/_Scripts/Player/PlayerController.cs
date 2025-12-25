using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public PlayerActionMode currentMode = PlayerActionMode.None;
    public Unit selectedUnit;

    public MoveRangeVisual moveRangeVisual;
    public AttackRangeVisual attackRangeVisual;
    public GameObject UnitActionPanel;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleClick();
    }

    // ================= SELECTION =================

    public void SelectUnit(Unit unit)
    {
        if (selectedUnit == unit)
            return;

        ClearSelection(false);

        selectedUnit = unit;
        UnitActionPanel.SetActive(true);
    }

    public void ClearSelection(bool hideUI = true)
    {
        moveRangeVisual.Clear();
        attackRangeVisual.Clear();

        selectedUnit = null;
        currentMode = PlayerActionMode.None;

        if (hideUI)
            UnitActionPanel.SetActive(false);
    }

    // ================= ACTION MODES =================

    public void SetMoveMode()
    {
        if (selectedUnit == null) return;

        currentMode = PlayerActionMode.Move;
        attackRangeVisual.Clear();
        moveRangeVisual.Show(selectedUnit);
    }

    public void SetAttackMode()
    {
        if (selectedUnit == null) return;

        currentMode = PlayerActionMode.Attack;
        moveRangeVisual.Clear();
        attackRangeVisual.Show(selectedUnit);
    }

    // ================= INPUT =================

    void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, 100f))
            return;

        // UNIT (Tank / Enemy)
        Unit unit = hit.collider.GetComponent<Unit>();
        if (unit != null)
        {
            // swój tank
            if (hit.collider.CompareTag("Tank"))
            {
                SelectUnit(unit);
                return;
            }

            // wróg
            if (hit.collider.CompareTag("Enemy"))
            {
                HandleEnemyClick(unit);
                return;
            }
        }

        // TILE
        Tile tile = hit.collider.GetComponent<Tile>();
        if (tile != null)
        {
            HandleTileClick(tile);
        }
    }

    // ================= HANDLERS =================

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
