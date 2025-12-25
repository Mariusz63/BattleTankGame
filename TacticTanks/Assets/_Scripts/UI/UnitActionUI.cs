using UnityEngine;

public class UnitActionUI : MonoBehaviour
{
    public static UnitActionUI Instance;

    public GameObject panel;
    public Vector3 worldOffset = new Vector3(0, 2f, 0);

    private Unit currentUnit;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if (currentUnit != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(
                currentUnit.transform.position + worldOffset
            );
            panel.transform.position = screenPos;
        }
    }

    public void Show(Unit unit)
    {
        currentUnit = unit;
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
        currentUnit = null;
    }

    // BUTTON EVENTS

    public void OnMoveClicked()
    {
        PlayerController.Instance.SetMoveMode();
        Hide();
    }

    public void OnAttackClicked()
    {
        PlayerController.Instance.SetAttackMode();
        Hide();
    }

    public void OnCancelClicked()
    {
        PlayerController.Instance.ClearSelection();
        Hide();
    }
}
