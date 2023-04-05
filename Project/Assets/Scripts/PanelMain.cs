
using UnityEngine;
using UnityEngine.SceneManagement;
public class PanelMain : MonoBehaviour {

    public GameObject panelContent;
    public GameObject mainObjects;

    public void OpenClosePanel(bool value) {
        panelContent.SetActive(value);
        mainObjects.SetActive(value);
    }
    
    public void PressButtonStart() {
        panelContent.SetActive(false);
        GameCore.instance.StartGame();
    }

    public void PressButtonExit() {
        Application.Quit();
    }

    public void PressButtonRestart() {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }



}
