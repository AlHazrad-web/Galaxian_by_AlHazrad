using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour {

    [HideInInspector] public static GameCore instance;

    [HideInInspector] public SoundsManager soundsManager;

    [HideInInspector] public Camera CameraMain;

    PanelMain panelMain;
    PanelGame panelGame;

    Bounds spawnbounds;
    
    bool ingame;
   
    float spawntime;   

    public int maxKilledEnemy = 30;
    public int killedEnemy = 0;

    List<Enemy> enemyes = new List<Enemy>();

    void Awake() {
        instance = this;
        soundsManager = GetComponent<SoundsManager>();
        panelMain =  gameObject.transform.Find("UI/Canvas/PanelMain").GetComponent<PanelMain>();
        panelGame = gameObject.transform.Find("UI/Canvas/PanelGame").GetComponent<PanelGame>();
        gameObject.transform.Find("Player").GetComponent<Player>().Init();
        CameraMain = gameObject.transform.Find("GameCamera").GetComponent<Camera>();
        spawnbounds = GetComponent<BoxCollider>().bounds;
        spawntime = Random.Range(0.5f, 2.0f);
    }

    public void StartGame() {
        Player.instance.gameObject.SetActive(true);
        panelMain.OpenClosePanel(false);
        panelGame.OpenClosePanel(true);
        soundsManager.StartGame();
        ingame = true;
    }


    void Update() {       
        if (ingame) {
            if (spawntime > 0.0f) {
                spawntime -= 1.0f * Time.deltaTime;
            }
            if (spawntime <= 0.0f) {
                spawntime = Random.Range(0.5f, 2.0f);
                SpawnEnemy();
            }      
        }        
    }

    public void EndGame(bool value) {
        if (ingame) {
            ingame = false;
            Player.instance.gameObject.SetActive(false);
            if (value) {
                panelGame.textWin.SetActive(true);
                panelGame.textFailure.SetActive(false);
                panelGame.panelEndGame.SetActive(true);
                for (int i = 0; i < enemyes.Count; i++) {
                    enemyes[i].Death();
                }
            } else {
                CreateEffect("Explosion", Player.instance.gameObject.transform.position);
                panelGame.textWin.SetActive(false);
                panelGame.textFailure.SetActive(true);
                panelGame.panelEndGame.SetActive(true);
            }
        }        
    }   

    void SpawnEnemy() {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Ships/Enemy_0" + Random.Range(1, 3)), RandomPointInBounds(), Quaternion.identity) as GameObject;
        enemyes.Add(enemy.GetComponent<Enemy>());
    }

    Vector3 RandomPointInBounds() {
        return new Vector3(
            Random.Range(spawnbounds.min.x, spawnbounds.max.x),
            Random.Range(spawnbounds.min.y, spawnbounds.max.y),
            Random.Range(spawnbounds.min.z, spawnbounds.max.z)
        );
    }

    public void KillEnemy(Enemy enemy) {
        enemyes.Remove(enemy);
        killedEnemy++;
        panelGame.AddScore();
        if (killedEnemy >= maxKilledEnemy) {
            EndGame(true);            
        }
    }
   
    public void CreateEffect(string effectname, Vector3 position) {
        GameObject effect = Instantiate(Resources.Load("Prefabs/Effects/" + effectname), position, Quaternion.identity) as GameObject;
        Destroy(effect, 2);
    }

  
}
