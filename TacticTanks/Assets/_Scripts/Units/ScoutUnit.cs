public class ScoutUnit : Unit
{
    protected override void Awake()
    {
        base.Awake();
        stats.movementRange += 2;
        stats.detectionRange += 2;
    }
}
