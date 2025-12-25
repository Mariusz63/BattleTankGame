using UnityEngine;

public class AttackAction : BaseAction
{
    public Unit attacker;
    public Unit target;

    public override void Execute()
    {
        if (attacker == null || target == null)
            return;

        target.TakeDamage(attacker.stats.damage);

        //prosta animacja strzału
        Vector3 start = attacker.transform.position + Vector3.up * 0.5f;
        Vector3 end = target.transform.position + Vector3.up * 0.5f;
        Debug.DrawLine(start, end, Color.red, 1f);
    }
}
