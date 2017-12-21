using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InterfaceFinal : MonoBehaviour
{
    public Text textLevel;
    public Text textThrown;
    public Text TextDestroyed;
    public Text textPoints;

    // Use this for initialization 
    void Start() { }

    // Update is called once per frame 
    void Update()
    {
        textLevel.text = "Level Reached " + GameManager.maxLevel;
        textThrown.text = "Total stones thrown: " + GameManager.currentNumberStonesThrown;
        TextDestroyed.text = "Total stones destroyed: " + GameManager.currentNumberDestroyedStones;
        textPoints.text = "Total Points Won: " + (GameManager.LevelPoints + GameManager.totalPoints);
    }

    // Button public 
    public void MyClick()
    {
        print("MY CLICK");
        SceneManager.LoadScene("Awake");
    }
}
