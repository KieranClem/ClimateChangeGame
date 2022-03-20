using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispencer : MonoBehaviour
{
    public int numberOfWaterToBeGiven;
    private bool nextToPlayer;
    private PlayerMovement player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nextToPlayer)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                player.numOfWater = numberOfWaterToBeGiven;
                player.UpdateWater();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            nextToPlayer = true;
            player = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nextToPlayer = false;
            player = null;
        }
    }
}
