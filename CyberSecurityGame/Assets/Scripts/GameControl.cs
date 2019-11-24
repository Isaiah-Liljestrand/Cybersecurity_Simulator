using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    public GameObject timeOfDay;
    private int Dollars;
    public GameObject Money;
    public GameObject Productivity;
    public int ProductivityNum;


    public float MinInfectWait;
    public float MaxInfectWait;
    private float ChosenInfectWait;
    private float PassedInfectTime;

    private float PassedDayTime;
    private int HourStep;

    private List<DiseaseType> AvailableDiseaseTypes;

    public float ActivationDistance; //Distance at which the player can investigate/talk.
    public GameObject ExclamationPrefab; //This is just a reference which will be instantiated in ComputerControl and EmployeeControl

    private bool Paused;

    // Start is called before the first frame update
    void Start()
    {
        Dollars = 1000;
        NC = GetComponent<NetworkControl>();
        NC.CreateLines(DeskObjects);
        ChosenInfectWait = Random.Range(MinInfectWait, MaxInfectWait);
        AvailableDiseaseTypes = new List<DiseaseType>() { DiseaseType.DOS, DiseaseType.Password, DiseaseType.Phish, DiseaseType.Upload };
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
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
            if (PassedDayTime > 30)
            {
                HourStep++;
                if (HourStep < 10)
                {
                    HourlyUpdate();
                    PassedDayTime = 0;
                }
                else
                {
                    //End Day
                }
            }
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
                PlayerObj.GetComponent<NavigateTo>().GoToPosition(hit.point, 1);
                HideNetwork();
            }
        }
    }

    public void PersonClicked(GameObject obj)
    {
        if (Vector3.Distance(PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {
            //Bring up UI stuff
            if (obj.GetComponent<EmployeeControl>().CanInvestigate) ;
            //GetComponent<DialogueControl>().startDialogue(obj.GetComponent<EmployeeControl>().DiseaseCode);
        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void ComputerClicked(GameObject obj)
    {
        if (Vector3.Distance(PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {
            //Bring up UI stuff
        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void PlayerComputerClicked(GameObject obj)
    {
        if (Vector3.Distance(PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {
            ShowNetwork();
        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void ObjectClicked(GameObject obj)
    {
        TargetObj.SetActive(true);
        TargetObj.transform.position = obj.transform.position;
        PlayerObj.GetComponent<NavigateTo>().GoToPosition(obj.transform.position, 4);
        //NC.set_infection_status(obj, 0);
        //ShowNetwork();
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
            List<EmployeeControl> CleanEmployees = new List<EmployeeControl>();
            foreach (GameObject Employee in EmployeeObjs)
            {
                if (Employee.GetComponent<EmployeeControl>().DiseaseCode == DiseaseType.Clean)
                {
                    CleanEmployees.Add(Employee.GetComponent<EmployeeControl>());
                }
            }
            //Ignoring stats for now.
            if (CleanEmployees.Count > 0)
            {
                int index = Random.Range(0, CleanEmployees.Count - 1);
                Debug.Log("Infecting " + index);
                //Call visual things
                DiseaseType ChosenDisease = AvailableDiseaseTypes[Random.Range(0, AvailableDiseaseTypes.Count - 1)];
                AvailableDiseaseTypes.Remove(ChosenDisease);
                float wait = 5;
                CleanEmployees[index].Infected(ChosenDisease, false, wait, true);
            }
        }
    }

    public void Pause()
    {
        Paused = true;
        foreach(GameObject Desk in DeskObjects)
        {
            Desk.GetComponent<ComputerControl>().Pause();
        }
        foreach (GameObject Employee in EmployeeObjs)
        {
            Employee.GetComponent<EmployeeControl>().Pause();
        }
    }

    public void Resume()
    {
        Paused = false;
        foreach (GameObject Desk in DeskObjects)
        {
            Desk.GetComponent<ComputerControl>().Resume();
        }
        foreach (GameObject Employee in EmployeeObjs)
        {
            Employee.GetComponent<EmployeeControl>().Resume();
        }
    }

    public void reduceProductivity()
    {
        ProductivityNum -= 20;
        Productivity.GetComponent<TextMeshProUGUI>().text = "$" + ProductivityNum + "/hr";
    }

    public void returnProductivity()
    {
        ProductivityNum += 20;
        Productivity.GetComponent<TextMeshProUGUI>().text = "$" + ProductivityNum + "/hr";
    }

    private void HourlyUpdate()
    {
        Dollars += ProductivityNum;
        Money.GetComponent<TextMeshProUGUI>().text = "$" + Dollars;
        string[] Times = new string[] { "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM"};
        timeOfDay.GetComponent<TextMeshProUGUI>().text = Times[HourStep];
    }
}
