using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    //Éä»÷¼ä¸ô
    private float ShootTime = 1.4f;
    //Éä»÷¼ÆÊ±Æ÷
    private float ShootTimer;
    //Íã¶¹·¢ÉäÎ»ÖÃ
    public Transform ShootTrans;
    //Íã¶¹×Óµ¯Ô¤ÖÆÌå
    public PeaBullet PeaPrefab;
    //¹¥»÷Åö×²Æ÷
    public ATKC ATKC;
    //¿ÉÒÔÉä»÷£¨ÓÉ¹¥»÷Åö×²Æ÷ÅÐ¶Ï£©
    public bool CanShoot;

    private void Awake()
    {
        ATKC.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    void Update()
    {
        switch (plantState)
        {
            case PlantState.Disable:
                DisableUpdate();
                break;
            case PlantState.Enable:
                EnableUpdate();
                break;
            default:
                break;
        }
    }
    public override void EnableUpdate()
    {

        if (CanShoot)
        {
            
            ShootTimer += Time.deltaTime;
            if (ShootTimer >= ShootTime)
            {
                ShootTimer = 0;
                GameObject.Instantiate(PeaPrefab, ShootTrans.position, Quaternion.identity);
                AudioControl.Instance.PlayClip(Config.shoot,0.1f);
            }
        }
        else
        {
            ShootTimer = 0;
        }

    }
}
