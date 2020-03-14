using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EducationControl : MonoBehaviour
{
    public EmployeeEducation[] education;

    private DiseaseControl DC;

    void Start()
    {
        education = new EmployeeEducation[16];
        DC = GetComponent<DiseaseControl>();
    }

    public bool EmployeeInfected(int employee, Disease disease)
    {
        //TODO:Implement immunity check as well
        if(education[employee].immunity == disease.distribute)
        {
            return false;
        }

        if(education[employee].canResistAttack)
        {
            if(UnityEngine.Random.Range(0, 2) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public int EmployeeSlowsDisease(Disease disease)
    {
        for(int i = 0; i < education.Length; i++)
        {
            if(education[i].slowsDiseaseSpread)
            {
                if(disease.infected_computers.Contains(i))
                {
                    return 1; //Amount having an employee with this modifier will slow the spread for each cycle
                }
            }
        }
        return 0;
    }

    public void EducateCure(int employee)
    {
        education[employee].canCureThemselves = true;
        //UI update
    }

    public void EducateResist(int employee)
    {
        education[employee].canResistAttack = true;
        //UI update
    }

    public void EducateImmunity(int employee, DistributeType type)
    {
        education[employee].immunity = type;
        //UI update
    }

    public void EducateInformation(int employee)
    {
        education[employee].informationBoost = true;
        //UI update
    }

    public void EducateSlowSpread(int employee)
    {
        education[employee].slowsDiseaseSpread = true;
        //UI update
    }
}
