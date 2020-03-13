using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTimeLineEventUI : MonoBehaviour
{
    public bool donefilling;
    public float fadespeed;
    public bool destroyonfade;
    private Animator anm;
    private bool fadingin;
    private bool fadingout;

    public Vector3 moveto;
    public float movespeed;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInChildren<Animator>())
            anm = GetComponentInChildren<Animator>();
        donefilling = false;
        fadingin = true;
        fadingout = false;
        foreach (Image img in GetComponentsInChildren<Image>())
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
    }

    public void Fill()
    {
        if (anm)
            anm.SetTrigger("fill");
    }

    public void Fade()
    {
        fadingout = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveto, movespeed * Time.deltaTime);

        if (anm)
        {
            if (anm.GetCurrentAnimatorStateInfo(0).IsName("UIFill_filled"))
                donefilling = true;
        }

        if (fadingout || fadingin)
        {
            bool destroyobj = false;
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                if (fadingout)
                {
                    if (img.color.a - (fadespeed * Time.deltaTime) < 0)
                    {
                        destroyobj = true;
                        break;
                    }
                }
                if (fadingin)
                {
                    if (img.color.a + (fadespeed * Time.deltaTime) > 1)
                    {
                        fadingin = false;
                        break;
                    }
                }
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + (fadespeed * Time.deltaTime * (fadingout ? -1 : 1)));
            }
            if (destroyobj)
                Destroy(this.gameObject);
        }
    }
}
