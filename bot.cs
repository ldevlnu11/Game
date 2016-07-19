using UnityEngine;
using System.Collections;

public class bot : MonoBehaviour {

    public Transform spawn; // об'єкт з якого спавнятся пулі
    private Animator player;
    private float healthPoint = 6f; // кількість здоров'я
    private float manaPoint = 3f; // мани
    private float spawnX, spawnY; // точки спавна(служать для респавна


    void Start() {
        player = GetComponent<Animator>();
        healthPoint = 6;
        manaPoint = 3;
        spawnX = transform.position.x;
        spawnY = transform.position.y;
    }

    void Update() {
        checkHealth();
    }

    void OnCollisionStay2D(Collision2D c) { // фізичне тіло
        if(c.gameObject.tag == "damageObj") { // якщо встаєш на пилу то урон

            damage();
        }
        if(c.gameObject.tag == "hit") { // якщо тіла торкається об'єкт з назвою fireball(Clone) то урон

            damage();
            Destroy(c.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D c2d) { // не фізичне тіло
        if(c2d.gameObject.tag == "hp" && healthPoint < 5f) { // якщо наступаєш на трігер з ім'ям hp і при цьому не більше 9 хп то додається 2 хп
            Destroy(c2d.gameObject);
            healthPoint += 2;
        } else if(c2d.gameObject.tag == "hp" && healthPoint == 5f) { // якщо поперднє але 9 хп то додається 1 хп
            Destroy(c2d.gameObject);
            healthPoint++;
        }
        if(c2d.gameObject.name == "coll") {
            //	respawn ();
            die();
        }
        if(c2d.gameObject.tag == "hit") { // якщо тіла торкається об'єкт з назвою fireball(Clone) то урон
            for(int i = 0; i < 6; i++) {
                damage();
            }
            Destroy(c2d.gameObject);
        }
    }
    void respawn() { // респаун
        player.SetBool("dead", false);
        transform.position = new Vector2(spawnX, spawnY); // точка респауну(в цьому випадку точка спауну)
        healthPoint = 6f; // кількість здоров'я яке дається при спауні
        manaPoint = 3f; // кількість мани
    }
    void die() { // смерть
        healthPoint = 0;
        manaPoint = 0;
        player.SetBool("dead", true);
    }
    void damage() { // урон
        if(healthPoint <= 6f && healthPoint > 0f) { // якщо не більше 10 хп і більше 0, то при уроні знімається 1 хп
            healthPoint = healthPoint - 1;
        }
    }
    void OnGUI() {
        GUI.Box(new Rect(0, 40, 100, 20), "HP: " + healthPoint); // хп на інтерфейсі
        GUI.Box(new Rect(0, 60, 100, 20), "MP: " + manaPoint); // мана на інтерфейсі
    }
    void checkHealth() {
        if(healthPoint == 0) {
            die(); //якщо 0 хп то смерть
        }
    }
}
