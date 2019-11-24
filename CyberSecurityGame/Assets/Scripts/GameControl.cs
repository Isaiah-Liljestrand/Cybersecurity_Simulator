using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject PlayerObj;
    public List<GameObject> EmployeeObjs;
    public List<GameObject> DeskObjects;
    public GameObject TargetObj;
    private NetworkControl NC;
    public GameObject NetworkShadow;
    public List<GameObject> BreakObjects;

    public float MinInfectWait;
    public float MaxInfectWait;
    private float ChosenInfectWait;
    private float PassedInfectTime;

    private float PassedDayTime;

    private List<DiseaseType> AvailableDiseaseTypes;

    // Start is called before the first frame update
    void Start()
    {
        NC = GetComponent<NetworkControl>();
        NC.CreateLines(DeskObjects);
        ChosenInfectWait = Random.Range(MinInfectWait, MaxInfectWait);
        AvailableDiseaseTypes = new List<DiseaseType>() { DiseaseType.DOS, DiseaseType.Password, DiseaseType.Phish, DiseaseType.Upload };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
        PassedDayTime += Time.deltaTime;
        PassedInfectTime += Time.deltaTime;
        if (PassedInfectTime > ChosenInfectWait)
        {
            FirstInfect();
            ChosenInfectWait = Random.Range(MinInfectWait, MaxInfectWait);
            PassedInfectTime = 0;
        }
    }

    private void Clicked()
    {
        //Check to see if the floor was clicked
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                TargetObj.SetActive(true);
                TargetObj.transform.position = hit.point;
                PlayerObj.GetComponent<NavigateTo>().GoToPosition(hit.point, 3);
            }
        }
    }

    public void ObjectClicked(GameObject obj)
    {
        TargetObj.SetActive(true);
        TargetObj.transform.position = obj.transform.position;
        PlayerObj.GetComponent<NavigateTo>().GoToPosition(obj.transform.position, 4);
        NC.set_infection_status(obj, 0);
        ShowNetwork();
    }

    public void ShowNetwork()
    {
        NetworkShadow.SetActive(true);
        NC.ShowLines();
        foreach(GameObject obj in DeskObjects)
        {
            obj.transform.Find("NetworkNode").gameObject.SetActive(true);
        }
    }

    public void HideNetwork()
    {
        NetworkShadow.SetActive(false);
        NC.HideLines();
        foreach (GameObject obj in DeskObjects)
        {
            obj.transform.Find("NetworkNode").gameObject.SetActive(false);
        }
    }

    private void FirstInfect()
    {
        if (AvailableDiseaseTypes.Count > 0)
        {
            //Choose a random employee and infect
            List<int> CleanEmployees = new List<int>();
            int i = 0;
            foreach (GameObject Employee in EmployeeObjs)
            {
                if (Employee.GetComponent<EmployeeControl>().DiseaseCode == DiseaseType.Clean)
                {
                    CleanEmployees.Add(i);
                }
                i++;
            }
            //Ignoring stats for now.
            if (CleanEmployees.Count > 0)
            {
                int index = Random.Range(0, 15);
                //Call visual things
                DiseaseType ChosenDisease = AvailableDiseaseTypes[Random.Range(0, AvailableDiseaseTypes.Count - 1)];
                AvailableDiseaseTypes.Remove(ChosenDisease);

                DeskObjects[CleanEmployees[index]].GetComponent<ComputerControl>().Infected(ChosenDisease, 30, true);
                EmployeeObjs[CleanEmployees[index]].GetComponent<EmployeeControl>().Infected(ChosenDisease, false);
            }
        }
    }
}
