using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerControl : MonoBehaviour
{
    public int Index;
    public GameObject ExclamationMark_prefab;
    private GameObject ExclamationMark;

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
}
