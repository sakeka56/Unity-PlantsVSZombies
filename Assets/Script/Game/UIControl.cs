using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public Animator animator;

    //准备-预制体
    public GameObject Prepare;

    //开始动画结束
    public bool startOver = false;

    //游戏结束UI预制体
    public GameObject OverUI;
    //窗口预制体
    public GameObject WinUI;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void UIStartOver()
    {
        Debug.Log("StartOver");
        startOver = true;
        Destroy(Prepare);
    }
    public void GameOver()
    {
        GameObject.Instantiate(OverUI);

        AudioControl.Instance.PlayClip(Config.lose_Music);
    }

    public void GameWin()
    {
        gameObject.GetComponent<Canvas>().sortingLayerName = "CurrentPlant";
        WinUI.SetActive(true);
    }
}
