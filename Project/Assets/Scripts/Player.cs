
using UnityEngine;

public class Player : MonoBehaviour {
    
    [HideInInspector] public static Player instance;

    public float damage = 40.0f;

    float rotspeed = 5.0f;
    GameObject gun;   

    GameObject bullet;

    public void Init() {
        bullet = Resources.Load("Prefabs/Effects/Bullet") as GameObject;
        gun = gameObject.transform.Find("gun").gameObject;
        instance = this;      
    }          

    void Update() {
        Vector3 worldPosition = GameCore.instance.CameraMain.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50.0f));       
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.FromToRotation(Vector3.forward, (worldPosition - transform.position).normalized), rotspeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0)) {
            Shot(worldPosition);
        }
    }

    void Shot(Vector3 worldPosition) {
        GameObject go = Instantiate(bullet, gun.transform.position, Quaternion.identity) as GameObject;      
        Bullet _Bullet = go.GetComponent<Bullet>();
        GameCore.instance.soundsManager.PlayOneShot(0);
        RaycastHit[] hits;
        Ray ray = GameCore.instance.CameraMain.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 100.0f);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider.gameObject.tag == "Enemy") {
                _Bullet.Shot(worldPosition, hits[i].collider.gameObject.GetComponent<Enemy>(), damage);
                return;               
            }
        }
        _Bullet.Shot(worldPosition, null, 0.0f);
    }



}
