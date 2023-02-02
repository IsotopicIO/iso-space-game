////////////////////////////////////////////////////////////////////////////
//
//  This singleton holds data about the current game, and is currently responsible for loading
//  and scrolling the map.
//             
//
////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;

    public ShipController ShipController;

    [Tooltip("The initial scrolling speed of the map, when the game starts")]
    public float MapStartingMovingSpeed = 10f;

    [Tooltip("Acceleration applied to the map as the game progresses")]
    public float MapMovingAcceleration = 5f;

    [HideInInspector] public float CurrentMovementSpeed = 0f;

    [Tooltip("A parent transform, under which all the map segments will be instantiated")]
    public Transform MapParent;

    [Tooltip("An array of MapSegment prefabs, holding all the possible map segments")]
    public MapSegment[] MapPrefabs;

    [Tooltip("How many map segments are loaded at a time")]
    public int MaxLoadedMapSegments;

    private LinkedList<MapSegment> mapSegments = new LinkedList<MapSegment>();

    // in game timer variables
    private float elapsedTime;
    private TimeSpan totalTime;
    private bool isTimerRunning;
    private IEnumerator timer;

    private void Awake()
    {
        //Singleton logic
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
            Debug.LogWarning($"Two instances of {nameof(GameManagement)}, which is a Singleton, found in scene.");
        }
    }

    private void Start()
    {
        //init in game timer
        isTimerRunning = false;
        elapsedTime = 0f;
        timer = CountUpScore();

        InitMap();
    }

    private void InitMap()
    {
        CurrentMovementSpeed = MapStartingMovingSpeed;
        MapSegment previousSegment = null;
        StartGameTimer();
        for (int i=0; i< MaxLoadedMapSegments; i++)
        {
            var segment = Instantiate(MapPrefabs[UnityEngine.Random.Range(0, MapPrefabs.Length)]);
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
        //Update and pply current map scrolling speed to all map segments 

        CurrentMovementSpeed += MapMovingAcceleration * Time.deltaTime;
        var deltaMovement = - CurrentMovementSpeed * Time.deltaTime * Vector3.forward;
        foreach (Transform t in MapParent.transform)
        {
            t.position += deltaMovement;
        }

        //Check if the first segment is behind the ship to remove it and instantiate the new one

        var firstSegment = mapSegments.First.Value;

        if (ShipController.transform.position.z - firstSegment.EndPoint.position.z > firstSegment.MaxOffsetFromPlayerBeforeRemove)
        {
            mapSegments.RemoveFirst();
            Destroy(firstSegment.gameObject);

            var newSegment = Instantiate(MapPrefabs[UnityEngine.Random.Range(0, MapPrefabs.Length)]);
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

        StopGameTimer();
        mapSegments.Clear();
        InitMap();
    }


    public void StartGameTimer()
    {
        ResetGameTimer();

        isTimerRunning = true;
        StartCoroutine(timer);
    }
    public void StopGameTimer()
    {
        isTimerRunning = false;
        StopCoroutine(timer);
    }
    private void ResetGameTimer()
    {
        elapsedTime = 0f;
    }

    private IEnumerator CountUpScore()
    {
        while (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            totalTime = TimeSpan.FromSeconds(elapsedTime);
            UIManager.Instance.SetScoreText(totalTime);
            yield return null;
        }
    }
}
