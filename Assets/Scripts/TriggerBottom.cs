using UnityEngine;
using System.Collections;

public class TriggerBottom : MonoBehaviour {

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag != "Shooter")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Trigger")
        {
            return; //Do nothing.
        }
    }
}
