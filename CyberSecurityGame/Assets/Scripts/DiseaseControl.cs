using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseControl : MonoBehaviour
{
    public List<Disease> possiblediseases;
    public List<Disease> diseases = new List<Disease>();
    public List<int> infectedcomputers = new List<int>();
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
        foreach(Disease d in diseases) {
            d.nextinfecttime--;

            //If an infection is to take place
            if(d.nextinfecttime == 0) {

                //Setting up next infect time
                d.nextinfecttime = d.NextInfect();

                //Get all connections from infected computers to others, then trim out computers already infected
                List<int> potentialinfections = network.GetComputersToInfect(d.GetInfected());
                //int count = 0;
                List<int> narrowedinfections = new List<int>();

                for(int i = 0; i < potentialinfections.Count; i++) {
                    foreach(int infected in infectedcomputers) {
                        if(potentialinfections[i] == infected) {
                            potentialinfections[i] = -1;
                            break;
                        }
                    }

                    if(potentialinfections[i] != -1) {
                        narrowedinfections.Add(potentialinfections[i]);
                    }
                }

                if(narrowedinfections.Count == 0) {
                    break;
                }

                potentialinfections = narrowedinfections;

                int infectedcomputer = potentialinfections[UnityEngine.Random.Range(0,potentialinfections.Count)];
                d.Infect(infectedcomputer);
                infectedcomputers.Add(infectedcomputer);
            }
        }
    }

    public void AddDisease(Disease disease, int computer)
    {
        disease.Infect(computer);
        diseases.Add(disease);
        disease.InitializeInfectTime();
        infectedcomputers.Add(computer);
    }

    public void CleanVirus(Disease disease)
    {
        foreach(int computer in disease.GetInfected()) {
            infectedcomputers.Remove(computer);
        }
        diseases.Remove(disease);
    }
}
