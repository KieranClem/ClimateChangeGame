using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Player movement
    Vector3 Movement;
    public Rigidbody rb;
    public float MovementSpeed = 10f;
    public float JumpForce = 5f;
    Vector3 Jump = new Vector3(0f, 2f, 0f);
    private bool isGrounded = true;

    //other gameplay elements
    private bool nextToSaveNPC;
    private SaveNPC NPCToSave;
    private int NPCsInLevel;
    private int NPCsSaved = 0;

    //UI Elements
    public Text hydrationLevel;
    public Text numOfWaterBottlesText;
    public Button waterUseButton;

    //Survival mechanics
    [HideInInspector] public int LevelOfWater = 5;
    public int numIncreaseForColWater;
    public int numOfWater;
    [HideInInspector]public bool inHeatZone = false;

    //checkpoint
    Transform checkpointPosition;


    // Start is called before the first frame update
    void Start()
    {
        hydrationLevel.text = "Hydration: " + LevelOfWater.ToString();
        NPCsInLevel = GameObject.FindGameObjectsWithTag("NPCToSave").Length;
        Debug.Log(NPCsInLevel);
    }

    // Update is called once per frame
    void Update()
    {
        //Get player's inputs
        Movement.x = Input.GetAxisRaw("Horizontal");

        if(nextToSaveNPC == true)
        {
            if(Input.GetKeyDown(KeyCode.E) && numOfWater >= 1 && (NPCToSave.saveFromHeat && !NPCToSave.saveFromFlood))
            {
                if (!NPCToSave.isSaved)
                {
                    //numOfWater -= 1;
                    //NPCToSave.GiveWater();
                    //NPCToSave.isSaved = true;
                    UseWater();
                    Debug.Log("things");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Jump * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        //Move player
        rb.MovePosition(rb.position + Movement * MovementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BottleOfWater")
        {
            numOfWater += 1;
            UpdateWater();
            Destroy(other.gameObject);
        }

        if (other.tag == "NPCToSave")
        {
            nextToSaveNPC = true;
            NPCToSave = other.GetComponent<SaveNPC>();
            NPCToSave.InteractionWithPlayer();
            if (NPCToSave.saveFromFlood && !NPCToSave.isSaved)
            {
                NPCsSaved += 1;
                NPCToSave.isSaved = true;
                CheckIfGameIsFinished();
            }
        }

        if(other.tag == "Floor")
        {
            isGrounded = true;
        }

        if(other.tag == "Checkpoint")
        {
            checkpointPosition = other.transform;
        }

        if(other.tag == "Water")
        {
            this.transform.position = checkpointPosition.position;
            if(numOfWater < 5)
            {
                numOfWater = 5;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPCToSave")
        {
            nextToSaveNPC = false;
            NPCToSave = null;
        }
    }

    public void UpdateWater()
    {
        numOfWaterBottlesText.text = numOfWater.ToString();
    }

    public void UpdateHydration()
    {
        
        hydrationLevel.text = "Hydration: " + LevelOfWater.ToString();
    }

    public void UseWater()
    {
        
        if (!nextToSaveNPC)
        {
            numOfWater -= 1;
            LevelOfWater += numIncreaseForColWater;
            UpdateHydration();
        }
        else if(nextToSaveNPC)
        {
            if (!NPCToSave.isSaved)
            {
                numOfWater -= 1;
                NPCToSave.GiveWater();
                NPCToSave.isSaved = true;
                NPCsSaved += 1;
                Debug.Log(NPCsSaved);
                CheckIfGameIsFinished();
                
            }
        }

        UpdateWater();
    }

    public IEnumerator DehydrationDamage()
    {

        while (inHeatZone)
        {

            yield return new WaitForSeconds(3f);
            
            //here to catch if player has left dehyration zone already and prevent them from taking damage
            if (inHeatZone)
            {
                LevelOfWater -= 1;
                UpdateHydration();
                Debug.Log("We here2");
            }

        }
    }

    void CheckIfGameIsFinished()
    {
        if (NPCsSaved >= NPCsInLevel)
        {
            //FinishLevel
            GetComponentInChildren<SceneLoads>().LoadMainMenu();
            Debug.Log(NPCsSaved);
        }
    }
}
