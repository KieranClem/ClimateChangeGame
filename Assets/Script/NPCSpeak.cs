using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSpeak : MonoBehaviour
{
    public string[] spokenDio;
    public bool savedNPC;

    public Text diolagueBox;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!savedNPC)
            {
                StartCoroutine(DisplayText(spokenDio[0]));
            }
            else
            {
                StartCoroutine(DisplayText(spokenDio[1]));
            }
        }
    }

    public IEnumerator DisplayText(string textToSay)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        screenPos.y += 30;
        diolagueBox.gameObject.SetActive(true);
        diolagueBox.text = textToSay;
        diolagueBox.transform.position = screenPos;

        yield return new WaitForSeconds(6f);

        diolagueBox.gameObject.SetActive(false);

    }

    public void Speak(bool Saved)
    {
        if(Saved)
        {
            StartCoroutine(DisplayText(spokenDio[1]));
        }
        else
        {
            StartCoroutine(DisplayText(spokenDio[0]));
        }
    }
}
