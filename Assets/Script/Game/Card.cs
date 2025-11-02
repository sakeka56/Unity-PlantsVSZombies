using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

//卡片状态
enum CardState
{
    Disable,
    Cooling,
    WaitingSun,
    Ready,
    Selected,
}

public class Card : MonoBehaviour
{


    [SerializeField]
    [Header("卡片状态")]
    private CardState cardState;
    [Header("卡片类型")]
    public PlantType plantType;

    public GameObject Card_light;
    public GameObject Card_dark;
    public Image Card_bk;

    [SerializeField]
    [Header("生成所需点数")]
    public uint needSunPoint;
    [SerializeField]
    [Header("冷却时间")]
    private float CDTime = 7.5f;
    private float CDTimer = 0;





    private void Awake()
    {
        cardState = CardState.Cooling;
    }

    void Update()
    {
        //判断状态
        switch (cardState)
        {
            case CardState.Cooling://等待冷却
                CoolingUpdate();
                break;
            case CardState.WaitingSun://等待阳光
                WaitingSunUpdate();
                break;
            case CardState.Disable:
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
        }
    }


    private void CoolingUpdate()
    {//冷却
        CDTimer += Time.deltaTime;
        Card_bk.fillAmount = (CDTime - CDTimer) / CDTime;
        if (CDTimer >= CDTime)
        {
            TransitionToWaitingSun();
            CDTimer = 0;
        }
    }

    private void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;
        Card_light.SetActive(false);
        Card_dark.SetActive(true);
        Card_bk.gameObject.SetActive(false);
    }
    private void WaitingSunUpdate()
    {//判断阳光是否足够
        if (needSunPoint <= SunManager.Instance.SunPoint)
        {
            TransitionToReady();
        }
    }

    public void TransitionToReady()
    {//将状态转换为准备
        cardState = CardState.Ready;
        Card_light.SetActive(true);
        Card_dark.SetActive(false);
        Card_bk.gameObject.SetActive(false);
    }

    private void ReadyUpdate()
    {//在准备时判断阳光是否充足，不够切换为等待阳光状态
        if (needSunPoint > SunManager.Instance.SunPoint)
        {
            TransitionToWaitingSun();
        }
    }
    public void TransitionToCooling()
    {//转换为冷却状态(放置后调用)
        cardState = CardState.Cooling;
        Card_light.SetActive(false);
        Card_dark.SetActive(true);
        Card_bk.gameObject.SetActive(true);
    }

    public void TransitionToSelected()
    {//转换为选中状态（改变卡片外观）
        cardState = CardState.Selected;
        Card_light.SetActive(true);
        Card_dark.SetActive(false);
        Card_bk.gameObject.SetActive(true);
        Card_bk.fillAmount = 1;
    }

    public void OnClick()
    {//卡牌被点击事件
        //将植物拿在手上
        bool isCurrentPlantNull =  HandManager.Instance.AddPlant(plantType,this);
        if (isCurrentPlantNull)
        {
            //卡片转换为选中模式
            TransitionToSelected();
            AudioControl.Instance.PlayClip(Config.seed);
        }
    }
}
