////////////////////////////////////////////////////////////////////////////
//
//  This component holds references and logic for a map segment.
//
//  Attach MapSegment to map segment game objects, make prefabs from them and add
//  their references to GameManagement -> MapPrefabs, for them to work.
//
//  Override this component for custom logic of special map segments.
//
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSegment : MonoBehaviour
{
    [Tooltip("An (empty) transform placed at the start of this map segment game object. Will connect with the previous segment's EndPoint")]
    public Transform StartPoint;

    [Tooltip("An (empty) transform placed at the end of this map segment game object. Will connect with the next segment's StartPoint")]
    public Transform EndPoint;

    [Tooltip("The maximum distance (on forward axis) between the player's ship and this segment's EndPoint, for the segment to be removed")]
    public float MaxOffsetFromPlayerBeforeRemove = 10f;

    public void SetPositionAfterSegment(MapSegment segment)
    {
        transform.position = segment.EndPoint.position + transform.position - StartPoint.position;
    }
}
