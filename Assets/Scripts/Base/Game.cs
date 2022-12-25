using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Game
{
    public int Balance { get; set; }
    public List<Business> Businesses = new List<Business>();

    internal void AddBalance(int value)
    {
        Balance += value;
    }

    public string GetSave()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Balance);
        foreach (var business in Businesses)
        {
            sb.Append(";" + business.GetSave());
        }
        return sb.ToString();
    }

    public bool TrySetSave(string data)
    {
        Debug.Log(data);
        try
        {
            var fields = data.Split(';');
            Balance = int.Parse(fields[0]);
            for (int i = 0; i < Businesses.Count; i++)
            {
                Businesses[i].SetSave(fields[1 + i]);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
