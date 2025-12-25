public class ArtilleryUnit : Unit
{
    protected override void Awake()
    {
        base.Awake();
        stats.attackRange += 3;
        stats.damage *= 1.5f;
    }
}
