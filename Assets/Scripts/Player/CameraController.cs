using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public ShipController ShipController;

    private Vector3 playerPositionOffset;

    private void Awake()
    {
        playerPositionOffset = transform.position - ShipController.transform.position;
    }
    private void Update()
    {
        transform.position = ShipController.transform.position + playerPositionOffset;
    }
}
