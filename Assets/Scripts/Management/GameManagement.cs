using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;

    public ShipController ShipController;

    public float MapStartingMovingSpeed = 10f;
    public float MapMovingAcceleration = 5f;
    [HideInInspector] public float CurrentMovementSpeed = 0f;

    public Transform MapParent;
    public MapSegment[] MapPrefabs;
    public int MaxLoadedMapSegments;
    private LinkedList<MapSegment> mapSegments = new LinkedList<MapSegment>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InitMap();
    }

    private void InitMap()
    {
        CurrentMovementSpeed = MapStartingMovingSpeed;
        MapSegment previousSegment = null;
        for (int i=0; i< MaxLoadedMapSegments; i++)
        {
            var segment = Instantiate(MapPrefabs[Random.Range(0, MapPrefabs.Length)]);
            segment.transform.parent = MapParent;
            if (i == 0)
            {
                segment.transform.position = MapParent.position;
            } else
            {
                segment.SetPositionAfterSegment(previousSegment);
            }
            segment.transform.position = new Vector3(ShipController.transform.position.x, segment.transform.position.y, segment.transform.position.z);

            previousSegment = segment;

            mapSegments.AddLast(segment);
        }
    }

    private void Update()
    {
        UpdateMap();
    }


    public void UpdateMap()
    {
        CurrentMovementSpeed += MapMovingAcceleration * Time.deltaTime;
        var deltaMovement = - CurrentMovementSpeed * Time.deltaTime * Vector3.forward;
        foreach (Transform t in MapParent.transform)
        {
            t.position += deltaMovement;
        }
        var firstSegment = mapSegments.First.Value;

        if (ShipController.transform.position.z - firstSegment.EndPoint.position.z > firstSegment.MaxOffsetFromPlayerBeforeRemove)
        {
            mapSegments.RemoveFirst();
            Destroy(firstSegment.gameObject);

            var newSegment = Instantiate(MapPrefabs[Random.Range(0, MapPrefabs.Length)]);
            newSegment.SetPositionAfterSegment(mapSegments.Last.Value);
            newSegment.transform.position = new Vector3(ShipController.transform.position.x, newSegment.transform.position.y, newSegment.transform.position.z);
            newSegment.transform.parent = MapParent;
            mapSegments.AddLast(newSegment);
        }
    }

    public void ResetLevel()
    {
        foreach (MapSegment segment in mapSegments)
        {
            Destroy(segment.gameObject);
        }

        mapSegments.Clear();
        InitMap();
    }
}
