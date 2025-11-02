using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKC : MonoBehaviour
{//ºÏ≤‚Õ„∂π«∞∑Ω «∑Ò”–Ω© ¨
    public PeaShooter PeaShooter;
    void Update()
    {
        if (PeaShooter.plantState == PlantState.Enable)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {

            PeaShooter.CanShoot = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {

            PeaShooter.CanShoot = false;

        }
    }
}
