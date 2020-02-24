using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerControl : MonoBehaviour
{

    public int Index;
    public GameObject ExclamationMark_prefab;
    public Disease disease;

    private GameObject ExclamationMark;
    //private bool isfixed;

    public bool CanInvestigate;

    public void CreateIssue()
    {
        ExclamationMark = Instantiate(ExclamationMark_prefab, this.transform);
        CanInvestigate = true;
    }

    public void SolveIssue()
    {
        Destroy(ExclamationMark);
        ExclamationMark = null;
        CanInvestigate = false;
    }

    public void FixComputer()
    {
        //open ui
        //
    }


    public void FixedResult(bool isfixed)
    {
        GameObject.Find("Control").GetComponent<GameControl>().ComputerFix(isfixed, disease);
    }
}
