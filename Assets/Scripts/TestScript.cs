using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Text Timer;

    public float timer = 0;
    public int deltaTImeTimeAdder = 1;
    public int fixeddeltaTImeTimeAdder = 1;
    public enum timeType
    {
        deltaTIme,
        fixedDeltaTIme
    }

    public timeType timeDiffer;
    public Coroutine testTimeMoveCo;

    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testTimeMoveCo = StartCoroutine(moveByTImeCO());
        timer = 0f;
    }

    int timerCnt = 0;
    IEnumerator moveByTImeCO()
    {
        Debug.Log("Test Start Here");

        if (this.timeDiffer == timeType.deltaTIme)
        {
            while(true)
            {
                while (this.gameObject.transform.position.x < 5)
                {
                    this.transform.position = new Vector3(
                        this.transform.position.x + moveSpeed,
                        this.transform.position.y,
                        this.transform.position.z);
                    yield return null;
                }
                timer = 0f;
                while (this.gameObject.transform.position.x > -5)
                {
                    this.transform.position = new Vector3(
                        this.transform.position.x - moveSpeed,
                        this.transform.position.y,
                        this.transform.position.z);
                    yield return null;
                }
                timer = 0f;
                yield return null;
            }

        }
        else if(this.timeDiffer == timeType.fixedDeltaTIme)
        {
            while (true)
            {
                while (this.gameObject.transform.position.x < 5)
                {
                    this.transform.position = new Vector3(
                        this.transform.position.x + moveSpeed,
                        this.transform.position.y,
                        this.transform.position.z);
                    yield return null;
                }
                timer = 0f;
                while (this.gameObject.transform.position.x > -5)
                {
                    this.transform.position = new Vector3(
                        this.transform.position.x - moveSpeed,
                        this.transform.position.y,
                        this.transform.position.z);
                    yield return null;
                }
                timer = 0f;
                yield return null;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (this.timeDiffer == timeType.deltaTIme)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                deltaTImeTimeAdder++;
                Timer.text = deltaTImeTimeAdder.ToString();
            }
            else if( Input.GetKeyDown(KeyCode.A))
            {
                deltaTImeTimeAdder--;
                Timer.text = deltaTImeTimeAdder.ToString();
            }
            moveSpeed = (timer += (deltaTImeTimeAdder * Time.deltaTime * 0.005f));
        }
        else if(this.timeDiffer == timeType.fixedDeltaTIme)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fixeddeltaTImeTimeAdder++;
                Timer.text = fixeddeltaTImeTimeAdder.ToString();
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                fixeddeltaTImeTimeAdder--;
                Timer.text = fixeddeltaTImeTimeAdder.ToString();
            }
            moveSpeed = (timer += (fixeddeltaTImeTimeAdder * Time.deltaTime * 0.005f));
        }

    }
}
