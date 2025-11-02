using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //是否已经有植物了
    public bool HadPlant;

    public GameObject BigCell;

    // Start is called before the first frame update
    private void Awake()
    {
        HadPlant = false;
        BigCell = GameObject.Find("BigCell");
    }
    private void OnMouseDown()
    {
        Debug.Log("点击了种植区域");
        //种植
        HandManager.Instance.SetPlant(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!HandManager.Instance.JudgeCurrentNull())
        {
            if (HadPlant == false)
            {
                if (collision.tag == "Plant")
                {

                    HandManager.Instance.StaticImage.GetComponent<SpriteRenderer>().enabled = true;
                    HandManager.Instance.StaticImage.transform.position =
                         new Vector3(this.transform.position.x, this.transform.position.y, 1);
                    HandManager.Instance.StaticImage.GetComponent<SpriteRenderer>().sortingLayerName = "Game";
                }
            }
            else
            {
                HandManager.Instance.StaticImage.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }


}
