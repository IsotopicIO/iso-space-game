using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShip : MonoBehaviour
{
    //private
    private float newPositionTimer;


    //pulic
    public float ChangePositionTimerMin = 0f;
    public float ChangePositionTimerMax = 6f;
    public float Health = 100f;
    public float HorizontalSpeedMultiplier = 0.6f;
    public float NextXOffsetMin = 10f;
    public float NextXOffsetMax = 70f;
    public float TargetXOffset { get; protected set; } = 0f;

    private void Awake()
    {
        TargetXOffset = transform.position.x;
        SetNextTimer();
    }

    private void Update()
    {
        UpdateMovement();
        if(this.Health <= 0)
        {
            Destroy(gameObject);
        }
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
