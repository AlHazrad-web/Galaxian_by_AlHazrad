using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 position;
    Enemy enemy;
    float damage;

    public void Shot(Vector3 position, Enemy enemy, float damage) {
        this.position = position;
        this.enemy = enemy;
        this.damage = damage;
    }
    
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, position, 200.0f * Time.deltaTime);
        if (Vector3.Distance(transform.position, position) <= 0.05f) {
            if (enemy != null) {
                enemy.Damage(damage);
            }
            Destroy(gameObject);
        }
    }


}
