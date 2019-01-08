using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour {

    [SerializeField]
    int dmgDone;

    //Type Of Dmg Done (used if we want hazards to be negated by shields)
    [SerializeField]
    public bool isPhysicalDmg;
    [SerializeField]
    public bool unBlockable;

    //check if colliding player is mage
    public bool isMageCheck(GameObject aPlayer)
    {
        bool isMage = aPlayer.GetComponent<BasePlayer>().isMage;
        return isMage;
    }

    public void damagePLayer(GameObject aPlayer)
    {
        if (isMageCheck(aPlayer))
        {
            if (unBlockable)
            {
                aPlayer.GetComponent<Mage>().TakeDamage(dmgDone, true);
            }

            else if (!aPlayer.GetComponent<Mage>().isInRock)
            {
                aPlayer.GetComponent<Mage>().TakeDamage(dmgDone, isPhysicalDmg);
            }
        }

        else
        {
            if (unBlockable)
            {
                aPlayer.GetComponent<Warrior>().TakeDamage(dmgDone, false);
            }
            else
            {
                aPlayer.GetComponent<Warrior>().TakeDamage(dmgDone, isPhysicalDmg);
            }
        }
    }
}
