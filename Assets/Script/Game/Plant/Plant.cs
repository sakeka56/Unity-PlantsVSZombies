using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//植物状态
public enum PlantState
{
    Disable,
    Enable,
}

//植物类型
public enum PlantType
{
    SunFlower,
    PeaShooter,
    WallNut,
}
public class Plant : MonoBehaviour
{
    //植物状态
    public PlantState plantState;
    //植物类型
    public PlantType plantType;

    //血量
    public float Hp;

    public Cell Cell;

    // Start is called before the first frame update
    void Start()
    {
        TransitionToDisable();
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
    public void DisableUpdate()
    {
    }
    public virtual void EnableUpdate()
    {
        //开始植物的行为
    }
    public void TransitionToDisable()
    {
        plantState = PlantState.Disable;
        GetComponent<Animator>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }
    public void TransitionToEnable()
    {
        plantState = PlantState.Enable;
        GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Game";
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void Eating(float atk)
    {//被吃
        this.Hp -= atk;
        if (Hp <= 0)
        {
            if (Cell!=null)
            {
                Cell.HadPlant = false;
            }
            Destroy(gameObject);
        }
    }
}
