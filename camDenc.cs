using UnityEngine;
using System.Collections;

public class camDenc : MonoBehaviour {

    public Camera camera;

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.tag == "hero")
        {
            camera.orthographicSize -= 0.05f;
        }
    }
}
