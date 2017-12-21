using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InterfaceStone : MonoBehaviour
{
    public Text textPoints;

    // Use this for initialization 
    void Start()
    { }

    // Update is called once per frame 
    void Update()
    {
        textPoints.text = "Points: " + (GameManager.LevelPoints + GameManager.totalPoints);
    }
}
