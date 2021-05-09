using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour {
    public GameObject spider;
    public GameObject mushroom;

    // Time delays for new enemy spawns.
    private const float MinSpawnTime = 1f;
    private const float SpiderTime = 7.5f;
    private const float MushroomTime = 4.0f;

	// Use this for initialization
	void Start () {
        // Get a seed and begin spawning each enemy based on its timer.
        Random.seed = (int)System.DateTime.Now.Ticks;
        InvokeRepeating("SpawnSpider", MinSpawnTime, SpiderTime);
        InvokeRepeating("SpawnMushroom", MinSpawnTime, MushroomTime);
    }
	
    void SpawnSpider()
    {
        Vector3 spiderSpawnPosition = new Vector3(Random.Range(-8, 10), Random.Range(-10, 10), 0);
        Instantiate(spider, spiderSpawnPosition, Quaternion.identity);
    }

    void SpawnMushroom()
    {
        Vector3 mushroomSpawnPosition = new Vector3(Random.Range(-8, 10), Random.Range(-10, 10), 0);
        Instantiate(mushroom, mushroomSpawnPosition, Quaternion.identity);
    }
}
