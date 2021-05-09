using UnityEngine;
using System.Collections;

public class DestroyOnEvent : MonoBehaviour {
    void OnEventDestroy(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
