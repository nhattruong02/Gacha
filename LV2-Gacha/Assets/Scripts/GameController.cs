using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] float maxSpeed;

    [SerializeField] float speed;

    [SerializeField] float speedDown;

    [SerializeField] Transform imgWheel;

    private bool stopWheel;

    [SerializeField] GameObject bgResult;

    [SerializeField] Text textUI;

    private float time;

    [SerializeField] List<string> prizes = new List<string>();

    [SerializeField] AudioSource audio;

    float finalAngle;

    float angle;

    private float targetAngle;

    [SerializeField] bool random;

    int indexRandom;

    [SerializeField] int round;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (stopWheel)
        {
            time -= Time.deltaTime;

            imgWheel.transform.Rotate(0, 0, speed * time *Time.deltaTime);

            speed -= speedDown;

            if (random)
            {
                finalAngle = imgWheel.transform.eulerAngles.z;

                if (finalAngle > targetAngle && finalAngle < targetAngle + angle && time < 1f)
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

        Debug.Log(indexRandom);

        targetAngle = angle * indexRandom; 

        time = Random.Range(4, 6);

        Debug.Log("Time"+ time);
        
        audio.Play();

        speed = maxSpeed;

        stopWheel = true;
    }
}
