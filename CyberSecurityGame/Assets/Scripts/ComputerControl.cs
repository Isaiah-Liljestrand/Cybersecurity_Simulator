using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerControl : MonoBehaviour
{
    public DiseaseType Disease;
    public float AttackWait;
    public bool Hidden;
    private float PassedTime;

    public void GetInfected(DiseaseType Disease, float AttackWait, bool Hidden)
    {
        this.Disease = Disease;
        this.AttackWait = AttackWait;
        this.Hidden = Hidden;
        PassedTime = 0;
    }

    private void Update()
    {
        if (Disease != DiseaseType.Clean)
        {
            PassedTime += Time.deltaTime;
            if (PassedTime > AttackWait)
            {
                //Do infecty stuff
                PassedTime = 0;
            }
        }
    }
}
