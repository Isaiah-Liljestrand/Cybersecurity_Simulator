using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyRotation : MonoBehaviour
{
    private Quaternion startrotation;

    private void Start()
    {
        startrotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startrotation;
    }
}
