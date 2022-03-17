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
            player.inHeatZone = true;
            StartCoroutine(player.DehydrationDamage());
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.inHeatZone = false;
            player = null;
        }
    }
}
