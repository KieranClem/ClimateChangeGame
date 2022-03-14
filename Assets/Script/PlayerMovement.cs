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

    //UI Elements
    public Text hydrationLevel;

    //Survival mechanics
    [HideInInspector] public int LevelOfWater = 5;
    public int numIncreaseForColWater;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWater();
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
            LevelOfWater += numIncreaseForColWater;
            UpdateWater();
            Destroy(other.gameObject);
        }
    }

    public void UpdateWater()
    {
        hydrationLevel.text = "Hydration: " + LevelOfWater.ToString();
    }
}
