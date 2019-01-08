using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {



    public void backToMainMenu() {

        SceneManager.LoadScene("Menu");

    }


    public void ReplayScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }



}
