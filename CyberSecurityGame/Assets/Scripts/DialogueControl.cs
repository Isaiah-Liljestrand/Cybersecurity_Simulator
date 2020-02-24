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
    private GameControl control;

    private List<string> Greeeting = new List<string>();
    private List<string> PhishProblem = new List<string>();
    private List<string> PhishInquiry = new List<string>();
    private List<string> PhishResponse = new List<string>();
    private List<string> UploadProblem = new List<string>();
    private List<string> UploadInquiry = new List<string>();
    private List<string> UploadResponse = new List<string>();
    private List<string> Dos_idkProblem = new List<string>();
    private List<string> Dos_idkInquiry = new List<string>();
    private List<string> Dos_idkResponse = new List<string>();
    private List<string> PasswdProblem = new List<string>();
    private List<string> PasswdInquiry = new List<string>();
    private List<string> PasswdResponse = new List<string>();
    private List<string> Conclusion = new List<string>();
    private int state;
    private DiseaseType virus;





    // Start is called before the first frame update
    void Start()
    {
        setConclusion();
        setGreeting();
        setPasswd();
        setPhish();
        setUpload();
        setDOS();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setGreeting()
    {
        Greeeting.Add("Hello fellow human of earth");
        Greeeting.Add("How are you doing today?");
        Greeeting.Add("What seems to be the issue?");
        Greeeting.Add("Hello!");
        Greeeting.Add("What's up?");
        Greeeting.Add("How is it going?");
        Greeeting.Add("Yo, what up?");
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
        PasswdProblem.Add("I haven't been able to log into my account for a few minutes");
        PasswdProblem.Add("I seem to be locked out of my account");
        PasswdProblem.Add("I got an email saying someone else logged into my account, now I can't log in.");
        PasswdInquiry.Add("Did you have a secure password? Was it very long or complex?");
        PasswdInquiry.Add("Sounds like your account was compromised, what kind of password were you using?");
        PasswdResponse.Add("I used my name and my birthday. It had a 3 instead of an E though.");
        PasswdResponse.Add("I used my street name and last name. I mixed up lowercase and capitals though.");
        PasswdResponse.Add("I'm not gonna lie, my password was password123.");
        PasswdResponse.Add("I used the name of my dog and then some numbers.");
    }

    private void setDOS()
    {
        Dos_idkProblem.Add("My computer seems to be unable to process requests and communicate with customers");
        Dos_idkProblem.Add("My network connection seems to be glitching out really badly");
        Dos_idkProblem.Add("I haven't been able to communicate normally for the last few minutes");
        Dos_idkProblem.Add("I think my computer is malfunctioning");
        Dos_idkInquiry.Add("Has there been anything weird happen to you or your computer before this issue started.");
        Dos_idkInquiry.Add("Have you brought in any foreign data or visited odd websites recently?");
        Dos_idkResponse.Add("I don't think so, it's been business as usual today.");
        Dos_idkResponse.Add("I don't think anything has been off today. I never did anything that goes against our company cyber security policy.");
    }

    private void setConclusion()
    {
        Conclusion.Add("Alright, that should be enough to work with, I'll let you know when everything is back in order.");
        Conclusion.Add("OK, I think that should be enough to figure this out, I'll get right on it.");
        Conclusion.Add("Hopefully, this shouldn't take too long to fix. I'll let you know what I find.");
        Conclusion.Add("We should be able to restore everything to how it was.");
        Conclusion.Add("I'll get right on it, this shouldn't take too long to fix. I'll keep you updated");
    }


    private string chooseGreeting()
    {
        //Random rand = new Random();
        int n = Random.Range(1, 7);
        return Greeeting[n - 1];
    }

    private string chooseDialogue()
    {
        int n; 
        switch (state)
        {
            case 1:
                switch (virus)
                {
                    case DiseaseType.DOS:
                        n = Random.Range(1, Dos_idkProblem.Count);
                        return Dos_idkProblem[n - 1];

                    case DiseaseType.Upload:
                        n = Random.Range(1, UploadProblem.Count);
                        return UploadProblem[n - 1];

                    case DiseaseType.Phish:
                        n = Random.Range(1, PhishProblem.Count);
                        return PhishProblem[n - 1];

                    case DiseaseType.Password:
                        n = Random.Range(1, PasswdProblem.Count);
                        return PasswdProblem[n - 1];
                }

                break;
            case 2:
                switch (virus)
                {
                    case DiseaseType.DOS:
                        n = Random.Range(1, Dos_idkInquiry.Count);
                        return Dos_idkInquiry[n - 1];

                    case DiseaseType.Upload:
                        n = Random.Range(1, UploadInquiry.Count);
                        return UploadInquiry[n - 1];

                    case DiseaseType.Phish:
                        n = Random.Range(1, PhishInquiry.Count);
                        return PhishInquiry[n - 1];

                    case DiseaseType.Password:
                        n = Random.Range(1, PasswdInquiry.Count);
                        return PasswdInquiry[n - 1];
                }
                break;
            case 3:
                switch (virus)
                {
                    case DiseaseType.DOS:
                        n = Random.Range(1, Dos_idkResponse.Count);
                        return Dos_idkResponse[n - 1];

                    case DiseaseType.Upload:
                        n = Random.Range(1, UploadResponse.Count);
                        return UploadResponse[n - 1];

                    case DiseaseType.Phish:
                        n = Random.Range(1, PhishResponse.Count);
                        return PhishResponse[n - 1];

                    case DiseaseType.Password:
                        n = Random.Range(1, PasswdResponse.Count);
                        return PasswdResponse[n - 1];
                }
                break;

        }
        return "Other";
        
    }

    private string chooseConclusion()
    {
        int n = Random.Range(1, Conclusion.Count);
        return Conclusion[n - 1];
    }

    public void Next()
    {
        switch (state)
        {

            case 1:
                Text2.GetComponent<TextMeshProUGUI>().SetText(chooseDialogue());
                bubble2.SetActive(true);
                state++;
                break;
            case 2:
                
                    bubble2.SetActive(false);
                    Text1.GetComponent<TextMeshProUGUI>().SetText(chooseDialogue());
                state++;
                break;
            case 3:
                Text2.GetComponent<TextMeshProUGUI>().SetText(chooseDialogue());
                bubble2.SetActive(true);
                state++;
                break;

            case 4:
                bubble2.SetActive(false);
                Text1.GetComponent<TextMeshProUGUI>().SetText(chooseConclusion());
                state++;
                break;

            case 5:
                //GetComponent<GameControl>().ConversationEnded();
                Investigation.SetActive(false);
                break;
        }
    }

    public void startDialogue(DiseaseType virus)
    {
        state = 0;
        this.virus = virus;
        Investigation.SetActive(true);
        Text1.GetComponent<TextMeshProUGUI>().SetText(chooseGreeting());
        bubble1.SetActive(true);
        state++;
        
    }
}
