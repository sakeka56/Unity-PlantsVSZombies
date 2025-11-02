using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullietHit : MonoBehaviour
{//豌豆子弹
    private void Start()
    {
        AudioControl.Instance.PlayClip(Config.hit,1);
    }
    public void Hit()
    {//击中，动画调用
        Destroy(gameObject);
    }
}
