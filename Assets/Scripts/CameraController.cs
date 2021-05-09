using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private float offset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Shooter");
        offset = transform.position.y - player.transform.position.y;
    }
	
	// Called after all other update functions.
	void LateUpdate () {
        transform.position = new Vector3(0, player.transform.position.y + offset, -10);
	}
}
