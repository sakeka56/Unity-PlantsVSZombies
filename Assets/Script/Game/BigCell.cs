using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class BigCell : MonoBehaviour
{
    public Collider2D Plant2D;
    private void OnMouseUp()
    {//ȡ����ֲ
        Debug.Log("�����ȡ������");
        Plant2D = null;
        HandManager.Instance.CancelPlant(); 
        PosZTo(1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {//��͸��ֲ����
            Plant2D = collision;
        }
        if (collision.name == "Zombie(Clone)")
        {//�����ֲ����ǰ�����ж���ATKC����
            collision.tag = "Zombie";
        }
    }
    private void Update()
    {
        if (Plant2D != null)
        {
            if (HandManager.Instance.StaticImage!=null)
            {
                Collider2D[] collider2Ds = new Collider2D[6];
                ContactFilter2D contactFilter2D = new ContactFilter2D();
                Plant2D.Overlap(contactFilter2D.NoFilter(), collider2Ds);
                foreach (var item in collider2Ds)
                {
                    if (item != null)
                    {
                        if (item.name == "Cell")
                        {
                            PosZTo(1);
                            return;
                        }
                    }

                }
                HandManager.Instance.StaticImage.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                PosZTo(-5);
            }


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == Plant2D)
        {
            collision = null;
        }
        PosZTo(1);
        if (collision!=null)
        {
            if (collision.tag == "ATK")
            {
                Destroy(collision.gameObject);
            }
        }

    }

    public void PosZTo(float z)
    {//�ı�Z��λ�ã�����ĳЩ���
        gameObject.transform.position
                                = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, z);
    }
}
