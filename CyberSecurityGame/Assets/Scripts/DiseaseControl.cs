using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseControl : MonoBehaviour
{
    public List<Disease> possiblediseases;
    public List<int> nextinfect;
    public List<Disease> diseases = new List<Disease>();
    private TimeLineControl timeline;
    private NetworkControl network;

    // Start is called before the first frame update
    void Start()
    {
        
        timeline = GetComponent<TimeLineControl>();
        network = GetComponent<NetworkControl>();
    }

    public void PassTurn()
    {
        //for(int i = 0; i < nextinfect.Count; i++) {
        //    nextinfect.IndexOf(i)--;
        //    if(nextinfect.IndexOf(i) == 0) {
        //       diseases.IndexOf(i).Infect()
        //        nextinfect.IndexOf(i) = diseases.IndexOf(i).NextSpread();
        //    }
        //}
    }

    public void AddDisease(Disease disease, int computer)
    {
        disease.Infect(computer);
        diseases.Add(disease);
        nextinfect.Add(disease.NextSpread());
    }

    public void CleanVirus(Disease disease)
    {
        Debug.Log("NIGGA");
    }
}
