using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script sets the value of the dezoomed parameter of the SharedCamera to the one decided by the user
 * when a player enters the collider.
 * Then, when that same player exits the collider, we set dezoomed to the previous value it was set to before entering the collider.
 */
public class ColliderChangeStateFollowInY : MonoBehaviour
{

    SharedCamera mainCam;

    [SerializeField]
    private bool newFollowInYVal = false;
    private bool previousFollowInY;

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
            previousFollowInY = mainCam.followInY;
            mainCam.followInY = newFollowInYVal;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == tagOfPlayerThatEnteredCollider)
        {
            mainCam.followInY = previousFollowInY;
            tagOfPlayerThatEnteredCollider = "";
        }
    }
}
