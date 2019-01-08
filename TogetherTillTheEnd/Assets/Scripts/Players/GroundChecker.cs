using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    Mage mage;
    Warrior warrior;
    public bool forMage = true;
    public GameObject player;
    public int index = 0;

    public AudioClip groundHitAudio;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (forMage)
            mage = player.GetComponent<Mage>();
        else
            warrior = player.GetComponent<Warrior>();
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "PushBlock")
        {
            //audioSource.PlayOneShot(groundHitAudio, 0.2f);
            if (forMage)
                mage.UpdateGroundCheck(true);
            else warrior.UpdateGroundCheck(true); 
        }

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "PushBlock")
        {
            if (forMage)
                mage.UpdateGroundCheck(true);
            else warrior.UpdateGroundCheck(true);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "PushBlock")
        {
            if (forMage)
                mage.UpdateGroundCheck(false);
            else warrior.UpdateGroundCheck(false);
        }
    }
}
