using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController3D : MonoBehaviour
{
    [SerializeField] float maxSpeed; 

    [SerializeField] float speed;

    [SerializeField] float speedDown;

    [SerializeField] GameObject imgWheel;

    private bool stopWheel;

    [SerializeField] GameObject bgResult;

    [SerializeField] Text textUI;

    private float time;

    [SerializeField] List<string> prizes = new List<string>();

    float finalAngle;

    float angle;

    private float target;

    [SerializeField] bool random;

    int indexRandom;

    private static GameController3D _instantce;

    public static GameController3D instantce => _instantce;

    [SerializeField] AudioSource audio;

    private void Awake()
    {
        _instantce = this;
    }

    private void FixedUpdate()
    {
        if (stopWheel)
        {

            time -= Time.deltaTime;

            imgWheel.transform.Rotate(0, 0, speed * time * Time.deltaTime);

            speed -= speedDown;

            if (random)
            {

                if (finalAngle < 0)
                {

                    finalAngle += 360;

                }

                if (finalAngle > target && finalAngle < target + angle && time < 1f)
                {

                    stopWheel = true;

                    showResult(finalAngle);

                }

            }
            else
            {
                if (time <= 0)
                {

                    finalAngle = imgWheel.transform.eulerAngles.z;
                    if(finalAngle < 0) {

                        finalAngle += 360;

                    }

                    showResult(finalAngle);
                }
            }

        }
    }

    private void showResult(float finalAngle)
    {
        audio.Stop();

        stopWheel = false;

        bgResult.SetActive(true);

        for (int i = 0; i < prizes.Count; i++)
        {

            if (finalAngle > i * angle && finalAngle < (i + 1) * angle)
            {

                textUI.text = prizes[i];

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (bgResult.activeSelf)
            {

                bgResult.SetActive(false);

            }
        }
    }
    public void Play()
    {
        angle = 360f / prizes.Count;

        indexRandom = Random.Range(0, prizes.Count);

        target = angle * indexRandom;

        time = Random.Range(4, 6);

        audio.Play();

        speed = maxSpeed;

        stopWheel = true;
    }
}
