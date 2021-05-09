using UnityEngine;
using System.Collections;

public class MushroomBehaviour : Enemy {
    private int _mushroomHp = 2; //Hitpoints
    private CircleCollider2D mushroomCollider; // Collider access to shrink mushroom when hit.
    
    // Public inspector objects.
    public Animator anim;
    public AudioClip mushroomHit;
    public AudioSource source;

    // Use this for initialization
    void Start () {
        // Setup inspector objects.
        anim = GetComponent<Animator>();
        mushroomCollider = GetComponent<CircleCollider2D>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Change the size of the mushroom and animation when hit.
    }
    
    // Deal damage to the mushroom.
    public override void TakeDamage()
    {
        source.PlayOneShot(mushroomHit);
        _mushroomHp -= 1;
        
        if (_mushroomHp == 1)
        {
            source.PlayOneShot(mushroomHit);
            mushroomCollider.radius = 0.52f;
            anim.SetTrigger("stage2");
        }
        else if (_mushroomHp == 0)
        {
            mushroomCollider.radius = 0.4f;
            anim.SetTrigger("stage3");
        }
        else if (_mushroomHp < 0)
        {
            anim.SetTrigger("death");
            Destroy(gameObject, 1f);
        }
    }
}
