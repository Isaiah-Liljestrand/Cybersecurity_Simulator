using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum DiseaseType
{
    Dos,
    Phish
}

[Serializable]
public class Disease
{
    public string diseasename;
    public DiseaseType diseasetype;
    public int turnsbeforespread;
    public Color color;
    public Sprite img;

    private int currentturn;

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
