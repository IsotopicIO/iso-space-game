using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSegment : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float MaxOffsetFromPlayerBeforeRemove = 10f;

    public void SetPositionAfterSegment(MapSegment segment)
    {
        transform.position = segment.EndPoint.position + transform.position - StartPoint.position;
    }
}
