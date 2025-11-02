using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant
{
    public Sun sunPrefab;

    //生产计时
    public float produceDuration ;
    public float produceTimer = 0;

    //动画
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        produceDuration = 18;
        produceTimer = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        TransitionToDisable();
    }

    public override void EnableUpdate()
    {//生成阳光
        produceTimer += Time.deltaTime;
        if (produceTimer >= produceDuration)
        {
            produceTimer = 0;
            animator.SetTrigger("isGlowing");

        }
    }
    public void ProduceSun()
    {//实例化一个阳光（动画事件）
        GameControl.Instantiate(sunPrefab, new Vector3(transform.position.x,transform.position.y,-5), transform.rotation);
    }
}
