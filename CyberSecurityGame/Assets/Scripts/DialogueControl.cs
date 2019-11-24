using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueControl : MonoBehaviour
{
    public GameObject Investigation;
    public GameObject Text1;
    public GameObject Text2;
    private GameControl control;

    private ArrayList Greeeting;
    private ArrayList PhishProblem;
    private ArrayList PhishInquiry;
    private ArrayList PhishResponse;
    private ArrayList UploadProblem;
    private ArrayList UploadInquiry;
    private ArrayList UploadResponse;
    private ArrayList Dos_idkProblem;
    private ArrayList Dos_idkInquiry;
    private ArrayList Dos_idkResponse;
    private ArrayList PasswdProblem;
    private ArrayList PasswdInquiry;
    private ArrayList PasswdResponse;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setPhish()
    {
        PhishProblem.Add("I got a weird email a few minutes ago and then my computer started acting weird.");
        PhishProblem.Add("My browser opened up this window in the background then my computer got really slow.");
        PhishProblem.Add("I was on social media and an old friend tried to friend me but when I opened their page my computer stopped responding.");
        PhishInquiry.Add("It sounds like you might have downloaded a virus. I'll have to look through and try to clean the computer.");
        PhishInquiry.Add("A virus might have infected your computer when that happened. I'll have to look further into it.");
        PhishResponse.Add("That's unfortunate, I'll have to be more wary of that kind of issue in the future");
        PhishResponse.Add("Oh no, I hope this doesn't take too long to fix, I have a lot of work to get done today.");
        PhishResponse.Add("Oh, well that explains it. This just keeps happening to me. I guess some people are just unlucky.");

    }

    private void setUpload()
    {
        UploadProblem.Add("My computer has been freezing quite a bit.");
        UploadProblem.Add("I've been having trouble accessing my files.");
        UploadProblem.Add("I haven't been able to login to my account.");
        UploadInquiry.Add("Did anything unusual happen on your computer or in the office today?");
        UploadInquiry.Add("Have you seen anything unusual on your computer recently or done anything different than usual?");
        UploadResponse.Add("I found an old USB near my desk and I checked to see if anything was on it but it turned out to just be some lame music. Other than that I don't think anything has been off.");
        UploadResponse.Add("I checked out a CD from the library and uploaded some music but other than that I don't think anything has been out of the usual.");
        UploadResponse.Add("There was a USB that wasn't mine in my computer when I came in. It didn't seem to have anything on it though.");
        UploadResponse.Add("I found my old USB near the food qube earlier, I plugged it in and it had some odd files on it. I cleaned it up and now it just has my usual files on it.");
    }

    private void setPasswd()
    {
        PasswdInquiry.Add("I haven't been able to log into my account for a few minutes");
        PasswdInquiry.Add("I seem to be locked out of my account");
        PasswdInquiry.Add("I got an email saying someone else logged into my account, now I can't log in.");
        PasswdProblem.Add("Did you have a secure password? Was it very long or complex?");
        PasswdProblem.Add("Sounds like your account was compromised, what kind of password were you using?");
        PasswdResponse.Add("I used my name and my birthday. It had a 3 instead of an E though.");
        PasswdResponse.Add("I used my street name and last name. I mixed up lowercase and capitals though.");
        PasswdResponse.Add("I'm not gonna lie, my password was password123.");
        PasswdResponse.Add("I used the name of my dog and then some numbers.");
    }

    private void setDOS()
    {
        Dos_idkInquiry.Add("My computer seems to be unable to process requests and communicate with customers");
        Dos_idkInquiry.Add("My network connection seems to be glitching out really badly");
        Dos_idkInquiry.Add("I haven't been able to communicate normally for the last few minutes");
        Dos_idkInquiry.Add("I think my computer is malfunctioning");
        Dos_idkProblem.Add("Has there been anything weird happen to you or your computer before this issue started.");
        Dos_idkProblem.Add("Have you brought in any foreign data or visited odd websites recently?");
        Dos_idkResponse.Add("I don't think so, it's been business as usual today.");
        Dos_idkResponse.Add("I don't think anything has been off today. I never did anything that goes against our company cyber security policy.");
    }

    private string chooseDialogue(DiseaseType code, int conv)
    {
        switch (code)
        {
            case DiseaseType.Clean:
                
                break;

        }
        return "Test";
    }


    public void Next()
    {

    }

    public void startDialogue()
    {
        //control.Pause();
        Investigation.SetActive(true);
        
        
    }
}
