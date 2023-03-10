////////////////////////////////////////////////////////////////////////////
//
//  (Deprecated)
//
//  This component will make the camera follow the ship as it moves through the scene.
//
//  Attach CameraController, to the main camera gameobject.              
//
////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("This component is obsolete. Camera controls have been moved to ShipController.cs")]
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
