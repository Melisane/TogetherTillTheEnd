using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAjustY : MonoBehaviour {

    SharedCamera mainCam;

    [SerializeField]
    private float deltaY = 1.5f;

    private string tagOfPlayerThatEnteredCollider = "";

    private Warrior warrior;
    private Mage mage;

    private void Start()
    {
        mainCam = FindObjectOfType<SharedCamera>();
        warrior = FindObjectOfType<Warrior>();
        mage = FindObjectOfType<Mage>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (tagOfPlayerThatEnteredCollider != "")
            return;
        if (collision.tag == "PlayerOne" || collision.tag == "PlayerTwo")
        {
            tagOfPlayerThatEnteredCollider = collision.tag;
            mainCam.changeY = deltaY;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == tagOfPlayerThatEnteredCollider)
        {
            mainCam.returnHome = true;
            tagOfPlayerThatEnteredCollider = "";
        }
    }
}
