using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameManagement gameManagement;

    public float TurningMaxSpeed = 30f;
    public float TurningAccceleration = 40f;
    public float TurningSpeedDamping = 20f;
    public float RotationToTurnSpeedRatio = 1.3f;

    public Camera shipCamera;
    public Vector3 cameraOffset;

    public ShipControllerInput CurrentInput = default;

    [HideInInspector] public float CurrentTurningSpeed = 0f;

    private void OnTriggerEnter(Collider other)
    {
        gameManagement.ResetLevel();
    }

    private void Update()
    {
        GetInput();
        Move();
        MoveCamera();
    }

    private void GetInput()
    {
        CurrentInput = new ShipControllerInput
        {
            TurnRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow),
            TurnLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)
        };
    }

    private void Move()
    {
        var turningSign = CurrentInput.GetTurningSign();
        CurrentTurningSpeed = Mathf.Clamp(CurrentTurningSpeed + ((turningSign == 0 && !Mathf.Approximately(CurrentTurningSpeed, 0f)) ? -Mathf.Sign(CurrentTurningSpeed) * TurningSpeedDamping * Time.deltaTime : turningSign * TurningAccceleration * Time.deltaTime), -TurningMaxSpeed, TurningMaxSpeed);

        transform.position += Vector3.right * CurrentTurningSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, -RotationToTurnSpeedRatio * CurrentTurningSpeed) * Quaternion.identity;
    }

    private void MoveCamera()
    {
        shipCamera.transform.position = transform.position + cameraOffset;
    }

    public struct ShipControllerInput
    {
        public bool TurnRight;
        public bool TurnLeft;

        public int GetTurningSign()
        {
            return (TurnRight ? 1 : 0) + (TurnLeft ? -1 : 0);
        }
    }
}
