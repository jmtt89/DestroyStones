using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLoop : MonoBehaviour
{
    public GameObject[] stones = new GameObject[3];
    public bool infinite = false;
    public float torque = 5.0f;
    public float minAntiGravity = 20.0f, maxAntiGravity = 40.0f;
    public float minLateralForce = -15.0f, maxLateralForce = 15.0f;
    public float minTimeBetweenStones = 3f, maxTimeBetweenStones = 5f;
    public float minX = -30.0f, maxX = 30.0f;
    public float minZ = -5.0f, maxZ = 20.0f;
    private bool enableStones = true;
    private Rigidbody _rigidbody;
    private Coroutine _actual;
    private int _actualLvl = 1;
    private int deltaLoose = 50;
    private Text _lName;
    // Use this for initialization 
    void Start()
    {
        GameManager.maxLevel = 1;
        GameManager.totalPoints = 0;
        GameManager.LevelPoints = 0;
        GameManager.currentNumberStonesThrown = 0;
        GameManager.currentNumberDestroyedStones = 0;
        _actualLvl = 1;
        if (infinite)
        {
            StartCoroutine(ThrowStones());
        }
        else
        {
            _lName = GameObject.Find(name: "LevelName").GetComponent<Text>();
            StartCoroutine(FadeOutCR());
        }

    }

    void FixedUpdate()
    {
        if (!infinite && GameManager.LevelPoints + (deltaLoose * _actualLvl) < 0)
        {
            GameManager.maxLevel = _actualLvl;
            SceneManager.LoadScene("Final");
        }
    }

    void LateUpdate()
    {
        if (Mathf.FloorToInt(Mathf.Sqrt(GameManager.LevelPoints / 100.0f)) > _actualLvl)
        {
            StopCoroutine(_actual);
            _actualLvl++;
            GameManager.totalPoints += GameManager.LevelPoints;
            GameManager.LevelPoints = 0;
            _lName.text = "Level " + _actualLvl;
            StartCoroutine(FadeOutCR());
        }
    }

    IEnumerator ThrowStones()
    {
        // Initial delay 
        yield return new WaitForSeconds(2.0f);
        while (enableStones)
        {
            GameObject stone = (GameObject)Instantiate(stones[Random.Range(0, stones.Length)]);
            stone.transform.position = new Vector3(Random.Range(minX, maxX), -30.0f, Random.Range(minZ, maxZ));
            stone.transform.rotation = Random.rotation;

            _rigidbody = stone.GetComponentInChildren<Rigidbody>();
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            _rigidbody.AddTorque(Vector3.up * torque, ForceMode.Impulse);
            _rigidbody.AddTorque(Vector3.right * torque, ForceMode.Impulse);
            _rigidbody.AddTorque(Vector3.forward * torque, ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.up * Random.Range(minAntiGravity, maxAntiGravity), ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.right * Random.Range(minLateralForce, maxLateralForce), ForceMode.Impulse);

            // Add a new thrown stone 
            GameManager.currentNumberStonesThrown++;
            var minTime = Mathf.Min(1f, minTimeBetweenStones - _actualLvl / 2.0f);
            var maxTime = Mathf.Max(maxTimeBetweenStones - _actualLvl / 3.0f, 2.5f);
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

    private IEnumerator FadeOutCR()
    {
        float duration = 2.5f; //2.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            _lName.color = new Color(_lName.color.r, _lName.color.g, _lName.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        _lName.text = "";
        _actual = StartCoroutine(ThrowStones());
        yield break;
    }
}