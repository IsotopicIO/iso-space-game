////////////////////////////////////////////////////////////////////////////
//
//  This component will enable flight controls on the gameobject it is placed on,
//  giving the player steering control of the ship, using A, D or LeftArrow, RightArrow
//
//  Attach ShipController to the ship game object.              
//
////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameManagement GameManagement;
    public Transform ShipVisualsParent;
    public Camera ShipCamera;

    [Tooltip("The max horizontal speed that the ship can reach.")]
    public float TurningMaxSpeed = 30f;

    [Tooltip("The horizontal acceleration applied to the ship when player moves.")]
    public float TurningAccceleration = 40f;

    [Tooltip("The horizontal acceleration applied towards the opposite side of movement, when there is no input, to slow the ship down.")]
    public float TurningSpeedDamping = 20f;

    [Tooltip("This is multiplied with the ship's current horizontal speed, to get the Z rotation of the ship, based on turning ship")]
    public float RotationToTurnSpeedRatio = 1.3f;

    public Vector3 CameraOffset;

    private Vector3 cameraVirtualOffset;
    public float maxCameraHorizontalOffsetAtSpeed = 1f;

    [SerializeField] private float maxFOV = 80;
    [SerializeField] private float speedWhenMaxFOV = 50;
    private float initialFOV;


    public ShipControllerInput CurrentInput = default;

    [HideInInspector] public float CurrentTurningSpeed = 0f;

    public float CurrentSpeed { get => GameManagement.CurrentMovementSpeed; }


    private void Awake()
    {
        initialFOV = ShipCamera.fieldOfView;
        cameraVirtualOffset = CameraOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManagement.ResetLevel();
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

        transform.position += CurrentTurningSpeed * Time.deltaTime * Vector3.right;

        ShipVisualsParent.rotation = Quaternion.Euler(0f, 0f, -RotationToTurnSpeedRatio * CurrentTurningSpeed) * Quaternion.identity;
    }

    private void MoveCamera()
    {
        cameraVirtualOffset = CameraOffset + CurrentTurningSpeed/TurningMaxSpeed * maxCameraHorizontalOffsetAtSpeed * Vector3.right;

        ShipCamera.transform.position = transform.position + cameraVirtualOffset;
        ShipCamera.fieldOfView = Mathf.Lerp(initialFOV, maxFOV, CurrentSpeed / speedWhenMaxFOV);
    }

    /// <summary>
    /// A struct holding information about a frame's input
    /// </summary>
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
