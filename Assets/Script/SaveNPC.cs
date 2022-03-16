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
    }

    void displayText()
    {
        words.Speak(isSaved);
    }
}
