using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Disease
{
    public string diseasename;
    public int turnsbeforespread;
    public int currentturn;
    public Color color;
    public Sprite img;

    private List<int> infected_computers;

    public void Turnpassed()
    {
        currentturn++;
        if (currentturn > turnsbeforespread)
        {
            Spread();
            currentturn = 0;
        }
    }

    public void Spread()
    {

    }

    public void Infect(int computer)
    {
        if (infected_computers == null)
            infected_computers = new List<int>();
        infected_computers.Add(computer);
    }

    public List<int> GetInfected()
    {
        return infected_computers;
    }
}
