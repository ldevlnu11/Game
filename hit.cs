using UnityEngine;
using System.Collections;

public class hit : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D c2d) {

        if(c2d.gameObject && c2d.gameObject.tag != "hero") { 

            Destroy(gameObject);
        }
        if(c2d.gameObject.tag == "lightBody" || c2d.gameObject.tag == "hero") {

            c2d.rigidbody.AddForce(new Vector2(6000, 100));
        }
    }

    void OnTriggerEnter2D(Collider2D c2d) {

        if(c2d.gameObject.name == "coll")
            Destroy(gameObject);
    }
}
