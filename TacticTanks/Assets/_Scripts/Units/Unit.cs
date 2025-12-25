using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Grid Position")]
    public int x;
    public int z;

    [Header("Stats")]
    public UnitStats stats;

    [Header("Runtime")]
    public float currentHP;

    protected virtual void Awake()
    {
        currentHP = stats.maxHP;
    }

    public void SetGridPosition(int newX, int newZ)
    {
        x = newX;
        z = newZ;

        transform.position = new Vector3(x, 0.5f, z);
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
