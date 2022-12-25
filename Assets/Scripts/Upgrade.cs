public class Upgrade
{
    public int Cost { get; private set; }
    public float Multiplier { get; private set; }
    public bool Purchased { get; set; }

    private Game _game;

    public UpgradeStatus Status
    {
        get
        {
            if (Purchased)
            {
                return UpgradeStatus.Purchased;
            }
            if (Cost <= _game.Balance)
            {
                return UpgradeStatus.AvailableForPurchase;
            }
            return UpgradeStatus.NotAvaliable;
        }
    }

    public void PurchaseUpgrade()
    {
        _game.AddBalance(-Cost);
        Purchased = true;
    }


    public Upgrade(int cost, float multiplier, Game game)
    {
        Cost = cost;
        Multiplier = multiplier;
        _game = game;
    }
}
