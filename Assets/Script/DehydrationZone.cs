using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DehydrationZone : MonoBehaviour
{
    private PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<PlayerMovement>();
            if(!player.inHeatZone)
            {
                player.inHeatZone = true;
                StartCoroutine(player.DehydrationDamage());
                Debug.Log("it hot");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player != null)
            {
                player.inHeatZone = false;
                player = null;
            }
        }
    }
}
