using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceAwake : MonoBehaviour
{

    void Awake()
    {
        GameManager.currentNumberDestroyedStones = 0;
        GameManager.currentNumberStonesThrown = 0;
    }

    public void Click()
    {
        SceneManager.LoadScene("StoneGame");
    }
}
