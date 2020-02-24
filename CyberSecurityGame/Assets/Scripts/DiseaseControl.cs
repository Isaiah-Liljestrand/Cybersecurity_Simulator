using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseControl : MonoBehaviour
{
    public List<Disease> diseases = new List<Disease>();
    private TimeLineControl timeline;

    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<TimeLineControl>();
    }

    public void AddDisease(Disease disease)
    {
        diseases.Add(disease);
    }

    public void AddDisease(Disease disease, int computer)
    {
        disease.Infect(computer);
        diseases.Add(disease);
    }

    public void CleanVirus(Disease disease)
    {

    }
}
