using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {
    public GameObject explosion;
    public int points = 2;
    private int multipler;
    private const float yDie = -30.0f;
    
    // Use this for initialization 
    void Start ()
    {
        // Set PowerUp
        multipler = Random.Range(1, 4);
        TextMesh t = GetComponentInParent<TextMesh>();
        if(t == null)
        {
            t = GetComponent<TextMesh>();
        }
        t.text = (multipler + "X");
        t.fontSize = 48;
        t.anchor = TextAnchor.MiddleCenter;
    }
    
    // Update is called once per frame 
    void Update () {
        if (transform.position.y < yDie)
        {
            GameManager.LevelPoints -= (points * multipler) / 2;
            Destroy(gameObject);
        }
    }

    // Let's detect a hit over a stone 
    private void OnMouseUpAsButton()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);

        // We have destroyed a stone 
        GameManager.LevelPoints += (points * multipler);
        GameManager.currentNumberDestroyedStones++;
    }

    void OnMouseDown()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);

        // We have destroyed a stone 
        GameManager.LevelPoints += (points * multipler);
        GameManager.currentNumberDestroyedStones++;
    }
}
