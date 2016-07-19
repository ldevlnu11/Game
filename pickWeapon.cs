using UnityEngine;
using System.Collections;

public class pickWeapon : MonoBehaviour {

    public Transform spawn;
    public int speed = 400;
    public KeyCode pick = KeyCode.F;
    private Rigidbody2D picked;
    private bool armed;
    private short face;

    void Start() {

        armed = false;
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.D)) {
            face = 1;
        }
        else if(Input.GetKeyDown(KeyCode.A))
            face = -1;
    }

    void OnTriggerStay2D(Collider2D c) {

        if(c.gameObject.tag == "pickable") {
            if(Input.GetKeyDown(pick) && !armed) {
                pickItem(c);
            }
        } else if(Input.GetKeyDown(KeyCode.G) && armed) {
            gameObject.transform.DetachChildren();
            picked.GetComponent<Rigidbody2D>().simulated = true;
            armed = false;
            if(face == 1) {
                picked.AddForce((new Vector2(1, 0)) * speed);
            } else if(face == -1) {
                picked.AddForce((new Vector2(-1, 0)) * speed);
            }
            picked = null;
        }
    }

    void pickItem(Collider2D c) {

        c.gameObject.transform.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
        c.gameObject.transform.position = gameObject.transform.position;
        c.gameObject.transform.SetParent(gameObject.transform);
        c.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        armed = true;
        picked = c.attachedRigidbody;
    }
}
