using System;
using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public float speed = 15f;
    private bool _isAlive = true;
    public Animator anim;

    private float reloadTime = .33f;

	// Use this for initialization.
	void Start () 
	{
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // Move the laser, if the laser dies freeze it while the death animation plays.
        if (_isAlive == true)
        {
            transform.Translate(0f, speed * Time.deltaTime, 0f);
        }
        else
        {
	        transform.Translate(0f, 0f, 0f);
        }
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// No more interactions can occur with this laser once triggered.
		// Kill it, play the death animation and remove the game object.
		gameObject.GetComponent<Collider2D>().enabled = false;
		_isAlive = false;
		anim.Play("laserdestroy");
		Destroy(gameObject, 1f);
		
		// let the enemy know it was hit.
		other.gameObject.GetComponent<Enemy>().TakeDamage();
	}
}
