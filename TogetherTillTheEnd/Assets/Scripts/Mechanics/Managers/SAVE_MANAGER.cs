using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SAVE_MANAGER : MonoBehaviour {

    Scene aScene;
    string aSceneName;

	// Use this for initialization
	void Start () {
        aScene = SceneManager.GetActiveScene();
        if(aScene.name != "MainMenu")
        {
            saveScene();
        }
    }
	
	public void loadScene()
    {
        aSceneName = PlayerPrefs.GetString("levelName");
        if(aSceneName != null)
        {
            Application.LoadLevel(aSceneName);
        }
    }

    public void saveScene()
    {
        PlayerPrefs.SetString("levelName", aScene.name);
    }
}
