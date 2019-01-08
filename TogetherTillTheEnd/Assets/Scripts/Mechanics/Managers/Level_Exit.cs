using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Exit : MonoBehaviour
{
    public string scene = "Level01-Graveyard";

    void OnCollisionEnter(Collision col)
    {
        PlayerPrefs.SetInt("CheckPoint", 0);
        Application.LoadLevel(scene);
    }

    public void LoadLevel(string Scene)
    {
        Application.LoadLevel(scene);
    }
}
