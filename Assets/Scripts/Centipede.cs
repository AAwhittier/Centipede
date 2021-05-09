using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Centipede : Enemy
{
    private float _speed = 5f;                                // Centipede speed.
    private int _length = 10;                                  // Length of centipede.
    private int _pathIndex = 0;                               // Centipedes pathing index.
    private Vector2 _heading = Vector2.right;                 // Current direction of the centipede.
    private List<Transform> _segments = new List<Transform>();// Centipede body parts.
    
    // Must be public to allow Unity control
    public GameObject[] path;                                 // Centipedes movement route.
    public GameObject segmentPrefab;                          // Centipede body segments.
    public GameObject headPrefab;                             // Centipede head prefab.
    
    // Start is called before the first frame update
    void Start()
    {
        // Start the centipede at the first path marker.
        transform.position = path[_pathIndex].transform.position;
        
        // Establish links to be able to control the Gui controls in Unity.
        // Build out the centipede to _length number of segments.
        for (int i = 0; i <= _length; i++)
        {
            SpawnSegment();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Split();
        Move();
        FollowPath();
    }

    private void FollowPath()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[_pathIndex].transform.position,
            _speed * Time.deltaTime); // Move towards the current path object.
       
        if (transform.position == path[_pathIndex].transform.position) // Increment path object when reached.
        {
            _pathIndex += 1;
        }
        if (_pathIndex == path.Length - 1)
        {
            _pathIndex = 0;
        }
        
    }
   private void Move()
   {
       Transform currentTransform = transform; // Save current location of head.

       // Turn towards the path.
       transform.right = path[_pathIndex].transform.position - transform.position;
       
       // Each centipede segment follows the segment in front.
       for(int i = 0; i < _segments.Count; i++)
       {
           if (i == 0)
           {
               // Follow the segment in front of the current segment.
               _segments[i].transform.position = Vector2.MoveTowards(_segments[i].transform.position,
                   currentTransform.position, AdjustSpeed(_segments[i].transform, currentTransform) * Time.deltaTime);
               // Rotate the sprite to face the segment it is following.
               _segments[i].transform.right = currentTransform.position - _segments[i].transform.position;
           }
           else
           {
               // Follow the segment before it in the list.
               _segments[i].transform.position = Vector2.MoveTowards(_segments[i].transform.position,
                   _segments[i-1].transform.position, AdjustSpeed(_segments[i].transform, _segments[i-1].transform) * Time.deltaTime);
               _segments[i].transform.right = _segments[i].transform.position - _segments[i-1].transform.position;
           }
       }
   }

   // Adjust the speed of each segment so that they stay close to the next segment.
   private float AdjustSpeed(Transform a, Transform b)
   {
       float distanceBetween = Vector2.Distance(a.position, b.position);
       if (distanceBetween < 1f)
       {
           return _speed - (float) 3.5;
       }
       else if (distanceBetween > 2f)
       {
           return _speed + (float) 1.5;
       }
       else
       {
           return _speed;
       }
   }
   
   public override void TakeDamage()
   {
       if (_segments.Count > 0)
       {
           Destroy(_segments[0].gameObject);
           _segments.RemoveAt(0);
       }
       else
       {
           Destroy(gameObject);
       }

       //Instantiate(gameObject, transform.position, quaternion.identity);

   }
   
   // Split the centipede on the destroyed cell into two centipedes with two separate heads.
   private void Split()
   {
       for(int i = 0; i < _segments.Count; i++)
       {
           // If segment is inactive it was hit.
           if (!_segments[i].gameObject.activeSelf)
           {
               // Remove the gameobject before removing from the list.
               Destroy(_segments[i].gameObject);
               _segments.RemoveAt(i);
               
               int count = 0;

               // Kill the disconnected centipede parts.
               for (int j = i; j < _segments.Count; j++)
               {
                   Destroy(_segments[j].gameObject);
                   _segments.RemoveAt(j);
                   count++;
               }
               // Create a new centipede of the length of the disconnected parts.
               GameObject centipede = Instantiate(gameObject, _segments[i-1].transform.position, quaternion.identity) as GameObject;
               centipede.GetComponent<Centipede>()._length = count;
               centipede.GetComponent<Centipede>()._pathIndex = _pathIndex - 1;

           }
       }
   }

   // Handles creating the centipede segments.
   private void SpawnSegment()
   {
       // Store the current position and then move to make room for new segment.
       Vector2 currentPosition = transform.position;
       transform.Translate(_heading * _speed * Time.deltaTime);

       // Create a new segment and add it to the Centipede.
       GameObject segment = Instantiate(segmentPrefab, currentPosition, quaternion.identity);
       _segments.Insert(_segments.Count, segment.transform);
   }
}
