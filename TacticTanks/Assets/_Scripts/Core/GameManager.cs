using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;
    public TankUnit tankPrefab;
    public ScoutUnit scoutPrefab;

    void Start()
    {
        if (!GridManager.Instance.IsTileOccupied(3, 5))
        {
            TankUnit tank = Instantiate(tankPrefab);
            tank.SetGridPosition(3,5);
        }

        // Spawnowanie Scoutu na pozycji (1,2)
        ScoutUnit scout = Instantiate(scoutPrefab);
        scout.SetGridPosition(1, 2);

    }

    void Awake()
    {
        Instance = this;
    }

    public void SetState(GameState newState)
    {
        State = newState;
    }


}
