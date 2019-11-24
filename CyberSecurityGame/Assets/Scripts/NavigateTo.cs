using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigateTo : MonoBehaviour
{
    public float GetWithinDistance;
    private Vector3 Target;
    private bool Walking;
    private Animator Anm;
    public NavMeshAgent Agent;

    // Start is called before the first frame update
    void Start()
    {
        Walking = false;
        Anm = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
    }

    public void GoToObject(GameObject obj, float Distance)
    {
        GoToPosition(obj.transform.position, Distance);
    }

    public void GoToPosition(Vector3 pos, float Distance)
    {
        GetWithinDistance = Distance;
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
}
