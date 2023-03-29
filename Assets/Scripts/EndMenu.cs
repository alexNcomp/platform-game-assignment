using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{

    public TMP_Text totalPoints;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        totalPoints.text = "Points: " + GameManager.instance.points;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        GameManager.instance.ResetPoints();
        GameManager.instance.SetPowerUp(false);
    }
}
