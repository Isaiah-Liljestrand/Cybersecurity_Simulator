using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

[Serializable]
public class Disease
{
    public string diseasename;
    public DiseaseType diseasetype;

    public int spreadtype;      //1 = static, 2 = list, 3 = range
    
    public int spreadspeedstatic;
    
    public int[] spreadspeedlist;
    public int spreadspeedlistindex;
    
    public int spreadspeedmax;
    public int spreadspeedmin;
    
    public Color color;
    public Sprite img;

    private List<int> infected_computers;

    public int NextSpread()
    {
        if(spreadtype == 1) {
            return spreadspeedstatic;

        } else if(spreadtype == 2) {
            if(spreadspeedindex >= spreadspeedlist.Length) {
                //Repeats last speed after previously specified length
                //Potential to change if this if this is to repeat the list or swap to random
                return spreadspeedlist[spreadspeedlist.Length - 1];
            }
            //spreadspeedlistindex should be initialized to 1
            return spreadspeedstatic[spreadspeedlistindex++];

        } else if(spreadtype == 3) {
            return UnityEngine.Random.Range(spreadspeedmin, spreadspeedmax + 1);
        }
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
