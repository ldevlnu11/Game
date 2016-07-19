using UnityEngine;
using System.Collections;

public class camInc : MonoBehaviour {

    public Camera camera;
    public GameObject denc;
    private float scaleX;
    private float scaleY;

    void Start() {
        scaleX = gameObject.transform.localScale.x;
        scaleY = gameObject.transform.localScale.y;
    }

    void OnTriggerStay2D(Collider2D c) {
        if(c.gameObject.tag == "hero") {
            camera.orthographicSize += 0.2f;
            scaleX += 0.05f;
            scaleY += 0.05f;
            denc.GetComponent<Collider2D>().transform.localScale = new Vector3(scaleX, scaleY);
            gameObject.GetComponent<Collider2D>().transform.localScale = new Vector3(scaleX, scaleY);
        }
    }
}
