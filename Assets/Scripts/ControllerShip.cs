using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ControllerShip : Enemy {

    private float playerSpeed = 5f;                   // Speed of player ship.
    private float reloadTime = .40f;                  // Time until next shot.
    private float elapsedTime = 0f;                   // Passed time.
    private SpriteRenderer _spriter;                   // Ship sprite.
    
    public GameObject laserPrefab;                    // Ship has a laser.
    public AudioClip laserSound;                      // Sound for the laser
    public AudioClip destroyed;
    private AudioSource _source;                       // Audio controller.

    // Initialization
	void Start () {
        _spriter = GetComponent<SpriteRenderer>();
        _source = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        transform.rotation = Quaternion.identity;

        // Move the player based on input.
        float xMovement = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float yMovement = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        transform.Translate(xMovement, yMovement, 0f);

        // Flip the sprite for a small animation when switching directions. 
        if (gameObject != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _spriter.flipX = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _spriter.flipX = true;
            }
        }
        
        // Fire laser.
        if (Input.GetButton("Jump") && elapsedTime > reloadTime)
        {
            _source.PlayOneShot(laserSound); // Play the laser sound.

            Vector3 spawnPosition = transform.position; // Laser starts just ahead of the ship.
            spawnPosition += new Vector3(0, 0.5f, 0);
            Instantiate(laserPrefab, spawnPosition, Quaternion.identity);

            elapsedTime = 0f;
        }
    }

    // Handle damage done to ship.
    public override void TakeDamage()
    {
        _source.PlayOneShot(destroyed);
        Vector3 center = new Vector3();
        transform.position = center;
    }
}
