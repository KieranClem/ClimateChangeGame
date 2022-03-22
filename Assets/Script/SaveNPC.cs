using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNPC : MonoBehaviour
{
    public bool isSaved = false;
    public bool saveFromFlood = true;
    public bool saveFromHeat;
    NPCSpeak words;
    
    // Start is called before the first frame update
    void Start()
    {
        words = this.GetComponent<NPCSpeak>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractionWithPlayer()
    {
        if(saveFromFlood)
        {
            isSaved = true;
            displayText();
            StartCoroutine(DestroySelf());

        }
        else if(saveFromHeat)
        {
            displayText();   
        }
    }

    public void GiveWater()
    {
        isSaved = true;
        displayText();
        StartCoroutine(DestroySelf());
    }

    void displayText()
    {
        words.Speak(isSaved);
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        words.HideText();
        Destroy(this.gameObject);
    }
}
