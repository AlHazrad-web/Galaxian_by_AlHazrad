
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float curHP;
    float speed = 4.0f;

    void Start() {
        GameCore.instance.CreateEffect("Spawn", transform.position);
    }

    void Update() {       
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z <= 0.0f) {            
            GameCore.instance.EndGame(false);
            return;
        }
    }

    public void Damage(float damage) {
        GameCore.instance.CreateEffect("Damage", transform.position);
        curHP -= damage;
        if (curHP <= 0.0f) {
            Death();
            GameCore.instance.KillEnemy(this);
        }
    }

    public void Death() {      
        GameCore.instance.CreateEffect("Explosion", transform.position);
        GameCore.instance.soundsManager.PlayOneShot(1);
        Destroy(gameObject);
    }

}
