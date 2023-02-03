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
    }

    public void SetScoreText(TimeSpan totalTime)
    {
        ScoreText.text = totalTime.ToString("m':'ss'.'ff");
    }

}
