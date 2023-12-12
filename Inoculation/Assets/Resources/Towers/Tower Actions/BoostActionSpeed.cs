using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostActionSpeed : MonoBehaviour
{

    public void OnTriggerStay2D(Collider2D collision) //it has to be OnTriggerStay and not OnTriggerEnter
    {                                                 //because a tower may not yet be placed down,
                                                      //and we need to constantly check for when this changes

        Debug.Log("Something entered Jimmy's sight");   //will clog the log, feel free to comment out
        // When a tower is within the booster's range
        if (collision.gameObject.tag == "tower")
        {
            Debug.Log("Jimmy sees a tower");
            if (collision.gameObject.GetComponent<Tower>().isPlaced == true && collision.gameObject.GetComponent<Tower>().isBoosted == false)
            {
                //boost speed
                if (collision.gameObject.TryGetComponent<ProjectileShoot>(out ProjectileShoot projshotscript))
                {
                    projshotscript.IncreaseSpeed();
                }

                if (collision.gameObject.TryGetComponent<PathSanitize>(out PathSanitize pathsanscript))
                {
                    pathsanscript.IncreaseSpeed();
                }

                collision.gameObject.GetComponent<Tower>().isBoosted = true;
                Debug.Log("JIMMY HAS BLESSED YOU WITH ADDITIONAL SPEED");
            }
            
        }
       
    }
}
