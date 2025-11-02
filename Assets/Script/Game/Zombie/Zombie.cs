using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieState
{
    Move,
    Eat,
    Dead
}
public class Zombie : MonoBehaviour
{
    [SerializeField]
    [Header("僵尸状态")]
    public ZombieState ZombieState;
    [SerializeField]
    [Header("攻击力")]
    private float Attack;
    [Header("血量")]
    public float Hp;
    [Header("移动速度")]
    private float MoveSpeed;

    private Plant eatingPlant;

    private float eatTime = 0.6f;
    private float eatTimer = 0f;

    public GameObject ZombieHead;

    private Animator animator;
    private bool cantMove;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        MoveSpeed = 0.21f;
        Attack = 60;
        Hp = 270;
        cantMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        HpUpdate();
        switch (ZombieState)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
            case ZombieState.Dead:
                DeadUpdate();
                break;
            default:
                break;

        }
    }
    private void MoveUpdate()
    {
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//判断是否为植物，转换为吃
        if (ZombieState == ZombieState.Move)
        {
            if (collision.gameObject.tag == "Plant")
            {
                if (collision.gameObject.GetComponent<Plant>().plantState == PlantState.Enable)
                {
                    eatingPlant = collision.gameObject.GetComponent<Plant>();
                    TransitionToEat();
                }
            }
        }
    }
    public void TransitionToEat()
    {
        //转换EATTING动画
        ZombieState = ZombieState.Eat;
        animator.SetBool("IsEating", true);

    }
    private void EatUpdate()
    {//进行攻击植物，并且判断植物血量是否为空
        eatTimer += Time.deltaTime;
        if (eatTimer>=eatTime)
        {
            eatTimer = 0;
            eatingPlant.Eating(Attack);
            AudioControl.Instance.PlayClip(Config.eat);
        }

        if (eatingPlant.Hp <= 0||eatingPlant == null)
        {
            eatingPlant = null;
            animator.SetBool("IsEating", false);
            TransitionToMove();
        }
    }
    public void TransitionToMove()
    {
        eatTimer = 0;
        ZombieState = ZombieState.Move;
    }
    public void HpUpdate()
    {//自身血量为0转换为死亡
        if (Hp<=0)
        {
            TransitionToDead();
        }
    }
    private void TransitionToDead()
    {
        //动画转换
        animator.SetTrigger("HPZero");
        ZombieState = ZombieState.Dead;
    }
    private void DeadUpdate()
    {
        //行走时死亡继续前进，吃的时候和倒地后不能前进
        if (cantMove == false)
        {
            if (eatingPlant == null)
            {
                MoveUpdate();
            }
        }

    }
    public void Fell()
    {
        //倒地
        cantMove = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }
    public void Die()
    {//动画调用
        Destroy(gameObject);
    }

    public void HeadPre()
    {//生成头部动画
        GameObject.Instantiate(ZombieHead,transform.position,Quaternion.identity);
    }
}
