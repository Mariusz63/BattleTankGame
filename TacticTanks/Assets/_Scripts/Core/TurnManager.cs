using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn = true;

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        GameManager.Instance.SetState(
            isPlayerTurn ? GameState.PlayerTurn : GameState.EnemyTurn
        );
    }
}
