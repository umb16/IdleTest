public class Upgrade
{
    public int Cost { get; private set; }
    public float Multiplier { get; private set; }
    public UpgradeStatus Status { get; set; }


    public Upgrade(int cost, float multiplier)
    {
        Cost = cost;
        Multiplier = multiplier;
    }
}
