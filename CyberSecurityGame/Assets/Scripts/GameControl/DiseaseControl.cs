using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseControl : MonoBehaviour
{
    public List<Disease> possiblediseases;

    public List<Disease> diseases = new List<Disease>();
    public List<int> infectedcomputers = new List<int>();
    public List<int> targetedcomputers = new List<int>();

    private TimeLineControl TC;
    private NetworkControl NC;
    private EducationControl EC;

    // Start is called before the first frame update
    void Start()
    {
        TC = GetComponent<TimeLineControl>();
        NC = GetComponent<NetworkControl>();
        EC = GetComponent<EducationControl>();
    }

    public void PassTurn()
    {   
        for(int i = 0; i < EC.education.Length; i++)
        {
            if(EC.education[i].canCureThemselves)
            {
                if(UnityEngine.Random.Range(0, 10) == 0) // Change this part to balance self-cleaning probability
                {
                    CleanComputer(i);
                }
            }
        }

        //Deals with disease spread
        foreach(Disease d in diseases) {
            d.nextinfecttime--;

            if(d.nextinfecttime < 0)
            {
                d.nextinfecttarget = GetComputerToInfect(d);

                if (d.nextinfecttarget != -1)
                {
                    //Setting up next infect time
                    d.nextinfecttime = d.NextInfect();
                    d.nextinfecttime += EC.EmployeeSlowsDisease(d);
                    targetedcomputers.Add(d.nextinfecttarget);
                }
            }
            //If an infection is to take place
            else if(d.nextinfecttime == 0) {

                targetedcomputers.Remove(d.nextinfecttarget);

                if (EC.EmployeeInfected(d.nextinfecttarget, d))
                {
                    d.Infect(d.nextinfecttarget);
                    infectedcomputers.Add(d.nextinfecttarget);
                }

                d.nextinfecttarget = GetComputerToInfect(d);

                if(d.nextinfecttarget != -1)
                {
                    //Setting up next infect time
                    d.nextinfecttime = d.NextInfect();
                    d.nextinfecttime += EC.EmployeeSlowsDisease(d);
                    targetedcomputers.Add(d.nextinfecttarget);
                }
            }
        }
    }

    private int GetComputerToInfect(Disease disease)
    {
        //Get all connections from infected computers to others, then trim out computers already infected
        List<int> potentialinfections = NC.GetComputersToInfect(disease.infected_computers);

        List<int> narrowedinfections = new List<int>();

        for (int i = 0; i < potentialinfections.Count; i++)
        {
            foreach (int infected in infectedcomputers)
            {
                if (potentialinfections[i] == infected)
                {
                    potentialinfections[i] = -1;
                }
            }

            foreach (int targeted in targetedcomputers)
            {
                if(potentialinfections[i] == targeted)
                {
                    potentialinfections[i] = -1;
                }
            }

            if (potentialinfections[i] != -1)
            {
                narrowedinfections.Add(potentialinfections[i]);
            }
        }

        if (narrowedinfections.Count == 0)
        {
            return -1;
        }

        return narrowedinfections[UnityEngine.Random.Range(0, potentialinfections.Count)];
    }

    public void AddDisease(Disease disease, int computer)
    {
        disease.Infect(computer);
        diseases.Add(disease);
        disease.InitializeInfectTime();
        disease.nextinfecttime += EC.EmployeeSlowsDisease(disease);
        List<int> potentialInfections = NC.GetComputersToInfect(disease.infected_computers);
        int nextInfection = potentialInfections[UnityEngine.Random.Range(0, potentialInfections.Count)];
        infectedcomputers.Add(computer);
    }

    public void CleanComputer(int computer)
    {
        foreach(Disease d in diseases) {
            if (d.infected_computers.Contains(computer))
            {
                d.infected_computers.Remove(computer);
                if(d.infected_computers.Count == 0)
                {
                    targetedcomputers.Remove(d.nextinfecttarget);
                    diseases.Remove(d);
                    break;
                }

                if(!NC.GetComputersToInfect(d.infected_computers).Contains(d.nextinfecttarget))
                {
                    targetedcomputers.Remove(d.nextinfecttarget);
                    d.nextinfecttarget = GetComputerToInfect(d);
                }
            }
        }
    }
}
