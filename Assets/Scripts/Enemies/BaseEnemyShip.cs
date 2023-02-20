using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShip : MonoBehaviour
{
    public float TargetXOffset { get; protected set; } = 0f;
    public float HorizontalSpeedMultiplier = 0.6f;

    private float newPositionTimer;

    public float ChangePositionTimerMin = 0f;
    public float ChangePositionTimerMax = 6f;

    public float NextXOffsetMin = 10f;
    public float NextXOffsetMax = 70f;

    private void Awake()
    {
        TargetXOffset = transform.position.x;
        SetNextTimer();
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        newPositionTimer -= Time.deltaTime;

        if (newPositionTimer <= 0)
        {
            TargetXOffset += (Random.Range(0, 2) == 0 ? 1 : -1) * Random.Range(NextXOffsetMin, NextXOffsetMax); 
            SetNextTimer();
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(TargetXOffset, transform.position.y, transform.position.z), Time.deltaTime * HorizontalSpeedMultiplier);
    }

    private void SetNextTimer()
    {
        newPositionTimer = Random.Range(ChangePositionTimerMin, ChangePositionTimerMax);
    }
}
