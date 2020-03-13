using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public GameObject TargetObj;
    public float taptime;
    public float ActivationDistance; //Distance at which the player can investigate/talk.

    private GameControl gamecontrol;
    private float holdingtime;
    private bool holdingclicked;
    private int fingerID = -1;

    private PlaySound playsound;

    private void Awake()
    {
#if !UNITY_EDITOR
        fingerID = 0; 
#endif
    }

    private void Start()
    {
        gamecontrol = GetComponent<GameControl>();
        playsound = GetComponent<PlaySound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamecontrol.IsPaused())
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdingclicked = true;
                holdingtime = 0;
            }
            if (holdingclicked)
            {
                holdingtime += Time.deltaTime;
                if (Input.GetMouseButtonUp(0))
                {
                    holdingclicked = false;
                    if (Validclick())
                        Clicked();
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
                PlayClick();
                TargetObj.SetActive(true);
                TargetObj.transform.position = hit.point;
                gamecontrol.PlayerObj.GetComponent<NavigateTo>().GoToPosition(hit.point, 1);
            }
        }
    }

    private bool Validclick()
    {
        if (EventSystem.current.IsPointerOverGameObject(fingerID))    // is the touch on the GUI
        {
            // GUI Action
            return false;
        }
        return !gamecontrol.IsPaused() && holdingtime <= taptime;
    }

    public void PersonClicked(GameObject obj)
    {
        if (!Validclick())
            return;
        if (Vector3.Distance(gamecontrol.PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {
            PlayMumbles();
            gamecontrol.PersonClicked(obj);
        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void ComputerClicked(GameObject obj)
    {
        if (!Validclick())
            return;
        if (Vector3.Distance(gamecontrol.PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {
            PlayKeyboardsounds();
            gamecontrol.ComputerClicked(obj);
        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void PlayerComputerClicked(GameObject obj)
    {
        if (!Validclick())
            return;
        if (Vector3.Distance(gamecontrol.PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {
            PlayKeyboardsounds();
            gamecontrol.PlayerComputerClicked(obj);
        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void ObjectClicked(GameObject obj)
    {
        if (!Validclick())
            return;
        PlayClick();
        TargetObj.SetActive(true);
        TargetObj.transform.position = obj.transform.position;
        gamecontrol.PlayerObj.GetComponent<NavigateTo>().GoToPosition(obj.transform.position, 1);
    }

    private void PlayClick()
    {
        if (playsound)
        {
            GetComponent<AudioSource>().volume = 0.6f;
            playsound.Play(0);
        }
    }

    private void PlayKeyboardsounds()
    {
        if (playsound)
        {
            GetComponent<AudioSource>().volume = 0.6f;
            playsound.Play(1);
        }
    }

    private void PlayMumbles()
    {
        if (playsound)
        {
            GetComponent<AudioSource>().volume = 1;
            playsound.PlayRandom(2);
        }
    }
}
