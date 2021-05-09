using UnityEngine;
using System.Collections;

public class SpiderBehaviour : Enemy {
    private GameObject _ship;               // Used to find the players location for tracking.
    // Inspector objects.
    public Animator anim;   
    public Transform target;
    public AudioClip hit;
    public AudioSource source;

    private const float MovementRange = 40f; // Maximum spider leap distance.
    private float moveTimer = .2f; // Time until spider can move again.
    private float elapsedTime = 0f; // Timer to track movement.
    private float speed = 50f; // Spider speed.

    private Vector3 velocity = Vector3.zero;  // Spider velocity.
    private float smoothTime = 0.05f;  // Smooth the animation over time.

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        
        // Get a new random seed and find the target.
        Random.seed = System.DateTime.Now.Millisecond;
        _ship = GameObject.FindGameObjectWithTag("Shooter");
        target = _ship.transform;
    }
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        Move();
	}

    //Move spider towards target position stored in target as a transform. 
    void Move ()
    {
        if (elapsedTime > moveTimer)
        {
            // Movement is bound to time rather than framerate.
            float xMovement = Random.Range(-MovementRange, MovementRange) * speed * Time.deltaTime;
            float yMovement = Random.Range(-MovementRange, MovementRange) * speed * Time.deltaTime;
            Vector3 movement = new Vector3(xMovement, yMovement, 0f);

            // Is the spider close enough to track the player.
            if (CompareVectors())
            {
                Vector3 targetPosition = target.position;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, movement, ref velocity, smoothTime);
            }
            elapsedTime = 0f;
        }
    }

    // Handle damage dealt to the spider.
    public override void TakeDamage()
    {
        source.PlayOneShot(hit);
        // Upon taking damage play death animation, disable collider then remove the object.
        anim.SetTrigger("spiderdeath");
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }

    // Handle collision with other game entitys.
    void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Enemy>().TakeDamage();
    }

    //Distance from target where spider will chase player.
    private bool CompareVectors()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        
        if(distance <= 10.0f)
        {
            return true;
        }
        return false;
    }
}
