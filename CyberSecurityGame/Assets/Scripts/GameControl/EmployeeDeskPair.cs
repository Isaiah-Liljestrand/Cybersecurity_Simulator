using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class EmployeeDeskPair
{
    public GameObject employee;
    public GameObject desk;

    public void Pause()
    {
        employee.GetComponent<EmployeeControl>().Pause();
    }

    public void Resume()
    {
        employee.GetComponent<EmployeeControl>().Resume();
    }
}
