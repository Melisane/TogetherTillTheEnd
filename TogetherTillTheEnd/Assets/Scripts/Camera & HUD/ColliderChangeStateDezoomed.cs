using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script sets the value of the followInY parameter of the SharedCamera to the one decided by the user
 * when a player enters the collider.
 * Then, when that same player exits the collider, we set followInY to the previous value it was set to before entering the collider.
 */
public class ColliderChangeStateDezoomed : MonoBehaviour
{

    SharedCamera mainCam;

    [SerializeField]
    private bool newDezoomedVal = false;

    private bool prevDezoomedVal;

    private string tagOfPlayerThatEnteredCollider = "";

    private void Start()
    {
        mainCam = FindObjectOfType<SharedCamera>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (tagOfPlayerThatEnteredCollider != "")
            return;
        if (collision.tag == "PlayerOne" || collision.tag == "PlayerTwo")
        {
            tagOfPlayerThatEnteredCollider = collision.tag;
            prevDezoomedVal = mainCam.dezoomed;
            mainCam.dezoomed = newDezoomedVal;
            mainCam.inSpecialState = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == tagOfPlayerThatEnteredCollider)
        {
            mainCam.dezoomed = prevDezoomedVal;
            mainCam.inSpecialState = false;
            tagOfPlayerThatEnteredCollider = "";
        }
    }
}
