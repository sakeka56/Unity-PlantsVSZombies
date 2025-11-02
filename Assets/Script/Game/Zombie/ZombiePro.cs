using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePro : MonoBehaviour
{
    public static ZombiePro Instance { get; private set; }

    public Zombie Zombie;
    [SerializeField]
    [Header("生成位置")]
    public List<Transform> ProPos = new List<Transform>();
    [Header("进攻僵尸列表")]
    public List<Zombie> ZombieAttackList = new List<Zombie>();

    [SerializeField]
    [Header("生成波次")]
    public int ZombieAttackCount;

    private int zombieAttackCount;

    [Header("生成僵尸数量")]
    public int ZombieCount;
    [Header("已生成僵尸数量")]
    public int ZombieProCount;
    [Header("生成时间")]
    public float proTime;
    public float ProTime
    {
        get
        {
            return this.proTime;
        }
        set
        {
            if (value <= 1)
            {
                this.proTime = 1;
            }
            else
            {
                this.proTime = value;
            }
        }
    }
    private float ProTimer = -20;


    private float subValue = 0.5f;
    private int zombieAttack;

    private void Awake()
    {
        proTime = 20;
        zombieAttack = 1;
        Instance = this;
    }
    private void Start()
    {
        zombieAttackCount = ZombieAttackCount * 2;
        this.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        ProTimer += Time.deltaTime;
        if (ProTimer>=ProTime)
        {
            ProTimer = 0;
            ProAZombie();
            AttackUpdate();
            ProTime -= subValue;

        }
    }
    public void AttackUpdate()
    {//进攻判断
        if (ZombieProCount == (int)(ZombieCount * ((float)zombieAttack / zombieAttackCount)))
        {
            ProZombie((int)(ZombieCount * (1f / zombieAttackCount)));

            if (zombieAttack == zombieAttackCount-1)
            {
                GameControl.Instance.TransitionToLastAttack();
                ProTime = 999;
            }
            zombieAttack += 2;
            subValue = zombieAttack / zombieAttackCount * (float)zombieAttackCount;
        }
    }

    public Zombie ProAZombie()
    {
        int index = Random.Range(0, ProPos.Count);
        float xR = Random.Range(2f, -1f);
        Zombie zombie = GameObject.Instantiate(Zombie,  
            new Vector3(ProPos[index].transform.position.x+xR, 
            ProPos[index].transform.position.y,
            ProPos[index].transform.position.z),
            Quaternion.identity);
        zombie.gameObject.GetComponent<SpriteRenderer>().sortingOrder =
            ProPos[index].GetComponent<SpriteRenderer>().sortingOrder;
        ZombieProCount++;
        return zombie;
    }
    public void ProZombie(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ZombieAttackList.Add(ProAZombie());
        }
    }
}
