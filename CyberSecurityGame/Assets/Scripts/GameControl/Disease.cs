using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Spreadtype
{
    stat,
    list,
    range
}




[Serializable]
public class Disease
{
    public string diseasename;
    public DiseaseType diseasetype;
    public DistributeType distribute;

    //Spread type and length
    public Spreadtype spreadtype;      //1 = static, 2 = list, 3 = range
    
    public int spreadspeedstatic;
    
    public int[] spreadspeedlist;
    private int spreadspeedlistindex;
    
    public int spreadspeedmax;
    public int spreadspeedmin;
    
    //UI information
    public Color color;
    public Sprite img;

    //Active infection information
    public int nextinfecttarget;
    public int nextinfecttime;
    public List<int> infected_computers;

    public void InitializeInfectTime()
    {
        if(spreadtype == Spreadtype.list) {
            nextinfecttime = spreadspeedlist[0];
            spreadspeedlistindex = 1;
        } else {
            nextinfecttime = NextInfect();
        }
    }

    public int NextInfect()
    {
        if(spreadtype == Spreadtype.stat) {
            return spreadspeedstatic;

        } else if(spreadtype == Spreadtype.list) {
            if(spreadspeedlistindex >= spreadspeedlist.Length) {
                //Repeats last speed after previously specified length
                //Potential to change if this if this is to repeat the list or swap to random
                return spreadspeedlist[spreadspeedlist.Length - 1];
            }
            //spreadspeedlistindex should be initialized to 1
            return spreadspeedlist[spreadspeedlistindex++];

        } else if(spreadtype == Spreadtype.range) {
            return UnityEngine.Random.Range(spreadspeedmin, spreadspeedmax + 1);
        }

        //Error. Added this for compilation purposes.
        return -1;
    }

    public void Infect(int computer)
    {
        if (infected_computers == null)
            infected_computers = new List<int>();
        infected_computers.Add(computer);
    }
}
