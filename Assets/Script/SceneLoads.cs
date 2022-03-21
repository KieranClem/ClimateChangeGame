using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoads : MonoBehaviour
{
    public void LoadWater()
    {
        SceneManager.LoadScene("WaterLevel");
    }

    public void LoadHeat()
    {
        SceneManager.LoadScene("HeatLevel");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
