using UnityEngine;
using System.Collections;

public class hit : MonoBehaviour {
	public Camera mainCam;
	void Start(){
		
	}
	void OnCollisionEnter2D(Collision2D c2d) {

		if (c2d.gameObject && c2d.gameObject.tag != "hero") { // якщо це об'єкт торкається будь-якого іншого то він самознищується
		Destroy(gameObject); 
				}
		if (c2d.gameObject.tag == "lightBody" || c2d.gameObject.tag == "hero" ) {
			c2d.rigidbody.AddForce (new Vector2(6000,100));
		}


	}
	void OnTriggerEnter2D(Collider2D c2d) {
		if (c2d.gameObject.name == "coll")
			Destroy (gameObject);
	}
	void OnDestroy(){
	}

	IEnumerator delay(int s){
		yield return new WaitForSeconds (s);

	}
}
