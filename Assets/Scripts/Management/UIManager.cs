using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;

    private float elapsedTime;
    private TimeSpan totalTime;
    private bool isTimerRunning;

    //create UIManager Instance for global referencing
    public static UIManager Instance;

    void Awake()
    {
        //Singleton logic
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning($"Two instances of {nameof(UIManager)}, which is a Singleton, found in scene.");
        }
    }

    private void Start()
    {
        isTimerRunning = false;
        elapsedTime = 0f;
    }

    public void StartGameTimer()
    {
        ResetGameTimer();

        isTimerRunning = true;
        StartCoroutine(CountUpScore());
    }
    public void StopGameTimer()
    {
        StopCoroutine(CountUpScore());
    }
    private void ResetGameTimer()
    {
        elapsedTime = 0f;
    }

    private IEnumerator CountUpScore()
    {
        while (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            totalTime = TimeSpan.FromSeconds(elapsedTime);
            ScoreText.text = totalTime.ToString("m':'ss'.'ff");
            yield return null;
        }
    }

}
