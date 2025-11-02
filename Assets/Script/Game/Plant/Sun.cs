using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//TODO:与僵尸刚体碰撞问题
public class Sun : MonoBehaviour
{
    //需要移动到左上角UI的位置
    private Vector3 sunPosition;
    //是否点击了
    private bool Clicked;
    [SerializeField]
    [Header("阳光值")]
    public uint point;

    //消失计时
    private float disTime = 5;
    private float disTimer = 0;

    //渲染（改变透明度）
    private SpriteRenderer spriteRenderer;
    private Color color;

    //下落计时
    private float downTime;
    private float downTimer = 0;
    //刚体
    public Rigidbody2D rb;

    //下落速度
    public float DownSpeed;

    private bool downOver;

    private bool IsSunFlowerProduce = true;

    private void Awake()
    {//初始化
        sunPosition = new Vector3(-5.81f, 4.5f, -5);
        Clicked = false;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    private void Start()
    {
        if (rb != null)
        {//向日葵生成
            //mass:1 subSpeed:0.85 up:3.5
            rb.AddForce(new Vector2(UnityEngine.Random.Range(-0.9f, 0.9f), 3.5f), ForceMode2D.Impulse);
            //不建议更改

        }
        else
        {//非向日葵生成
            DownSpeed = 1f;
            downTime = UnityEngine.Random.Range(2f, 6.5f);
            IsSunFlowerProduce = false;
        }
    }
    void Update()
    {

        TranslateUpdate();
        DestroyUpdate();
        if (IsSunFlowerProduce)
        {
            JumpOutUpdate();
        }
        else
        {
            DownUpdate();
        }
        if (HandManager.Instance.JudgeCurrentNull() == false)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }

    }

    private void OnMouseDown()
    {//点击，非卡片选中状态执行位置更新
        if (HandManager.Instance.JudgeCurrentNull())
        {
            Debug.Log("点击了阳光");
            Clicked = true;
            Destroy(rb);
            rb = null;
            AudioControl.Instance.PlayClip(Config.sun);
        }

    }
    private void TranslateUpdate()
    {//点击后位置更新
        if (this.Clicked)
        {

            color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
            if (transform.position != sunPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, sunPosition, 10 * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
                SunManager.Instance.AddSun(point);
            }
        }
    }
    private void DestroyUpdate()
    {//阳光消失
        if (!this.Clicked)
        {
            if (downOver)
            {
                disTimer += Time.deltaTime;
                if (disTimer > disTime)
                {
                    color = spriteRenderer.color;
                    color.a -= 0.5f * Time.deltaTime;
                    spriteRenderer.color = color;
                    if (color.a <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }

        }
    }
    public void JumpOutUpdate()
    {//默认抛物线生成
        if (this.Clicked == false)
        {
            if (rb != null)
            {

                if (rb.gravityScale > 0)
                {
                    rb.gravityScale -= 0.85f * Time.deltaTime;
                }
                else
                {
                    downOver = true;
                    Destroy(rb);
                    rb = null;
                }
            }
        }
    }
    public void DownUpdate()
    {//下落
        if (this.Clicked == false)
        {
            if (downTimer < downTime)
            {
                downTimer += 1f * Time.deltaTime;
                transform.Translate(Vector3.down * Time.deltaTime * DownSpeed);
            }
            else
            {
                downOver = true;
            }
        }
    }
}
