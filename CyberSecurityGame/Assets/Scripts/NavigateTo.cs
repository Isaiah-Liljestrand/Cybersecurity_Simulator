using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigateTo : MonoBehaviour
{
    public float GetWithinDistance;
    public GameObject Test;
    public GameObject Test2;
    private Vector3 Target;
    private bool Walking;
    private Animator Anm;
    private NavMeshAgent Agent;

    // Start is called before the first frame update
    void Start()
    {
        Walking = false;
        Anm = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        if (Test)
            GoToObject(Test);
    }

    public void GoToObject(GameObject obj)
    {
        GoToPosition(obj.transform.position);
    }

    public void GoToPosition(Vector3 pos)
    {
        Target = pos;
        Walking = true;
        Anm.SetBool("Walking", true);
        Agent.isStopped = false;
        Agent.SetDestination(Target);
    }

    // Update is called once per frame
    void Update()
    {
        if (Walking)
        {
            if (Vector3.Distance(transform.position, Target) < GetWithinDistance)
            {
                Walking = false;
                Anm.SetBool("Walking", false);
                Agent.isStopped = true;
            }
        }
    }

    private void OnMouseDown()
    {
        GoToObject(Test2);
    }
}
