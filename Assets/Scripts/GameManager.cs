using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public int points = 0;
    public bool powerUp = false;

    private void Awake() 
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddPoints(int points)
    {
        this.points += points;
        Debug.Log("Points: " + this.points);
    }

    public void ResetPoints()
    {
        this.points = 0;
    }

    public void SetPowerUp(bool condition)
    {
        this.powerUp = condition;
        Debug.Log("Power Up");
    }
}
