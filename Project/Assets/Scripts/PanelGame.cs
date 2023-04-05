
using UnityEngine;
using UnityEngine.UI;
public class PanelGame : MonoBehaviour {
  
    public GameObject panelContent;
    public GameObject panelEndGame;
    public GameObject textWin;
    public GameObject textFailure;
    public Text score;

    public void Start() {     
        score.text = GameCore.instance.killedEnemy + "/" + GameCore.instance.maxKilledEnemy;
    }
    public void OpenClosePanel(bool value) {
        panelContent.SetActive(value);
    }
    public void AddScore() {      
        score.text = GameCore.instance.killedEnemy + "/" + GameCore.instance.maxKilledEnemy;
    }

}
