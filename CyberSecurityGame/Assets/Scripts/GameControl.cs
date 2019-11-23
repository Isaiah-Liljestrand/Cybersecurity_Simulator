using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject PlayerObj;
    public List<GameObject> EmployeeObjs;
    public List<Stats> EmployeeStats;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
    }

    void Clicked()
    {
        //Check to see if the floor was clicked
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                Target.SetActive(true);
                Target.transform.position = hit.point;
                PlayerObj.GetComponent<NavigateTo>().GoToPosition(hit.point);
            }
        }
    }

    public void PlayerReachedSpot()
    {
        Target.SetActive(false);
    }
}
