using UnityEngine;

[CreateAssetMenu(menuName = "Tactical Game/Unit Stats")]
public class UnitStats : ScriptableObject
{
    public int movementRange;
    public int attackRange;
    public int detectionRange;

    public float damage;
    public float maxHP;
}
