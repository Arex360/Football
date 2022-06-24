using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    private bool over;
    private void Start()
    {
        instance = this;
        DisplayTime(timeRemaining);
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                if (!over)
                {
                    GameObject _slider = GameObject.FindObjectOfType<Slider>().gameObject;
                    _slider.SetActive(false);
                    if(GameManger.instance.noOfGoals > GameManger.instance.noOfFails)
                    {
                        GameManger.instance.endScreen.showWinScreen();
                    }
                    else
                    {
                        GameManger.instance.endScreen.showLoseScreen();
                    }
                    over = true;
                }
            }
        }
    }
    public void StartTimer()
    {
        timerIsRunning = true;
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}