using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public UIControl UIControl;
    public Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void GameOver()
    {//¶¯»­¿ØÖÆ
        UIControl.GameOver();
    }
}
