using UnityEngine;
using System.Collections;
using System.Threading;


public class Controll : MonoBehaviour { /** ЩОБ ОБ'ЄКТ МОЖНА БУЛО ЗНИЩИТИ, ТРЕБА НАЗВАТИ ЙОГО hero */
	private float maxSpeed = 25;	// швидкість бігу
	private float jumpForce = 15000f;  // сила стрибка
	public bool facingRight = false; // при спавні куда дивиться персонаж
	private float move; // считує напрям і ввід з клавіатури
	private float healthPoint = 6f; // кількість здоров'я
	private float manaPoint = 3f; // мани
	private float spawnX,spawnY; // точки спавна(служать для респавна
	public Rigidbody2D rightBullet; // стріла яка вилітає на право
	public Rigidbody2D leftBullet; // на ліво
	public Transform spawn; // об'єкт з якого спавнятся пулі
	private float speed = 3000f; // швидкість пуль
	public bool stunned = false; // чи є під станом,якщо так то не можна стріляти
	private Animator player;
	private enum ProjectAxis {onlyX = 0, xAndY = 1};
	private ProjectAxis projectAxis = ProjectAxis.onlyX;
	public KeyCode attack = KeyCode.Space;
	public KeyCode jump = KeyCode.W;
	public KeyCode pause = KeyCode.Escape;
	private int forUnstun = 0;
    

	void Start () {
		player = GetComponent<Animator> ();
		healthPoint = 6;
		manaPoint = 3;
		stunned = false;
		spawnX = transform.position.x;
		spawnY = transform.position.y;
	}
	void FixedUpdate () {
		if (!stunned) {
						move = Input.GetAxis ("Horizontal"); // записує у змінну данні вводу. якщо направо то = 1, наліво -1
			if (move != 0)
				player.SetBool("run",true);
			else
				player.SetBool("run",false);
						GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y); // сила руху
				}
	}

	void Update(){

		checkHealth ();
		if (stunned)
			player.SetBool ("stun", true);
		if (Input.GetKey (attack))
			forUnstun++;
		if (forUnstun == 100) {
			stunned = false;
			forUnstun = 0;
		}
		else if (!stunned)
			player.SetBool ("stun", false);
		if(move > 0 && !facingRight) Flip(); else if(move< 0 && facingRight) Flip();
		if (Input.GetKeyDown (KeyCode.Space) && !stunned) { // якщо нажати пробіл і не під станом то йде постріл
			player.SetBool("attack",true);
			shoot();


			 

		} else if(Input.GetKeyUp(attack)) player.SetBool("attack",false);



	}
	void Flip()
	{
		if(projectAxis == ProjectAxis.onlyX)
		{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
	void OnCollisionStay2D (Collision2D c) { // фізичне тіло
		
		if(c.gameObject && !stunned && c.gameObject.tag != "dontJump") { // якщо торкаєшся будь-який об'єкт з колайдером то можна стрибати 
			if(Input.GetKey(jump) && !stunned){
				GetComponent<Rigidbody2D>().AddForce (new Vector2(0f,jumpForce));
				player.SetBool ("jump", true);
			}
//		StartCoroutine (delay (1));


		}
		if(c.gameObject.tag == "hit") { // якщо тіла торкається об'єкт з назвою fireball(Clone) то урон
			damage();
			Destroy(c.gameObject);
		}
	}
	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject && !stunned && c.gameObject.tag != "dontJump") {
		player.SetBool("jump",false);
		}
        if (c.gameObject.tag == "damageObj")
        { // якщо встаєш на пилу то урон
            //healthPoint -= 1;
            damage();
        }
	}
	void OnTriggerStay2D(Collider2D c2d) { // не фізичне тіло
		if (c2d.gameObject.tag == "hp" && healthPoint < 5f) { // якщо наступаєш на трігер з ім'ям hp і при цьому не більше 9 хп то додається 2 хп
			Destroy (c2d.gameObject);
			healthPoint += 2;
		} else if (c2d.gameObject.tag == "hp" && healthPoint == 5f) { // якщо поперднє але 9 хп то додається 1 хп
			Destroy (c2d.gameObject);
			healthPoint++;
		}
		if (c2d.gameObject.name == "coll") {
		//	respawn ();

			die();
		}
		if (c2d.gameObject.tag == "hit") {
			die ();
		}
		}
	void respawn(){ // респаун
		player.SetBool("dead",false);
		stunned = false;
		transform.position = new Vector2 (spawnX, spawnY); // точка респауну(в цьому випадку точка спауну)
		healthPoint = 6f; // кількість здоров'я яке дається при спауні
		manaPoint = 3f; // кількість мани

	}
	void die() { // смерть
		healthPoint = 0;
		manaPoint = 0;
		player.SetBool("dead",true);
		stunned = true;
        Destroy(gameObject);
	}
	void damage() { // урон
		if (healthPoint <= 6f && healthPoint >0f) { // якщо не більше 10 хп і більше 0, то при уроні знімається 1 хп
			healthPoint -=1; 
		} 
	}
	void shoot() { // постріл
		if (facingRight && !stunned) { // якщо не під станом то стріляєш у сторону куда дивишся
			//magicRight.Play;
			Rigidbody2D clone = Instantiate (rightBullet, spawn.position, Quaternion.identity) as Rigidbody2D;
			clone.AddForce (new Vector2 (speed, 0f));

		} else if (!facingRight && !stunned) {
		//	magicLeft.Play;
			Rigidbody2D clone = Instantiate (leftBullet, spawn.position, Quaternion.identity) as Rigidbody2D;
			clone.AddForce (new Vector2 (-speed+1, 0f));
		}
	}
	IEnumerator delay(int s){
		yield return new WaitForSeconds(s);

	}
	void OnGUI(){
		GUI.Box (new Rect (0, 0, 100, 20), "HP: " + healthPoint); // хп на інтерфейсі
		GUI.Box (new Rect (0, 20, 100, 20), "MP: " + manaPoint); // мана на інтерфейсі
	}

	void checkHealth(){
		if(healthPoint == 0 && !stunned || stunned){
			die (); //якщо 0 хп то смерть
	}
}
}


