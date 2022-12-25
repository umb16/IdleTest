using System.Collections.Generic;

public class Game
{
    public int Balance { get; set; }
    public List<Business> Businesses = new List<Business>();

    internal void AddBalance(int value)
    {
        Balance += value;
    }
}
