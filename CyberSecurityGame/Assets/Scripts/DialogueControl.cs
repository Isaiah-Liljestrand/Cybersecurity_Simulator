using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueControl : MonoBehaviour
{
    public GameObject Investigation;
    public GameObject bubble1;
    public GameObject bubble2;
    public GameObject Text1;
    public GameObject Text2;
    public The_Dialouge_text main_script;


    private GameControl control;
    private int state;
    private DiseaseType virus;





    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string chooseDialogue()
    {

        return "Other";
        
    }

   

   
    public void startDialogue(DiseaseType virus)
    {
        state = 0;
        this.virus = virus;
        Investigation.SetActive(true);
        Text1.GetComponent<TextMeshProUGUI>().SetText(chooseDialogue());
        bubble1.SetActive(true);
        state++;
        
    }
}
