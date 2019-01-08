using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex = 1;

    void OnTriggerEnter(Collider col)
    {
        PlayerPrefs.SetInt("CheckPoint", checkpointIndex);
    }
}
