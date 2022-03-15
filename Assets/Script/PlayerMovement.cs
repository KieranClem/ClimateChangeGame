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

    //other gameplay elements
    private bool nextToSaveNPC;
    private GameObject NPCToSave;

    //UI Elements
    public Text hydrationLevel;
    public Text numOfWaterBottlesText;
    public Button waterUseButton;

    //Survival mechanics
    [HideInInspector] public int LevelOfWater = 5;
    public int numIncreaseForColWater;
    public int numOfWater;

    // Start is called before the first frame update
    void Start()
    {
        hydrationLevel.text = "Hydration: " + LevelOfWater.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Get player's inputs
        Movement.x = Input.GetAxisRaw("Horizontal");
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

        if(other.tag == "NPCToSave")
        {
            nextToSaveNPC = true;
            NPCToSave = other.gameObject;
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
        numOfWater -= 1;
        if (!nextToSaveNPC)
        {
            LevelOfWater += numIncreaseForColWater;
            UpdateHydration();
        }

        UpdateWater();
    }
}
