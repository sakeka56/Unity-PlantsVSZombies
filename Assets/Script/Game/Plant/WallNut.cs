using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{

    private enum WallNutState
    {
        OK = 0,
        NotGood = 1,
        Bad = 2,
    }
    [SerializeField]
    [Header("¼á¹û×´Ì¬")]
    private WallNutState state;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        switch (state)
        {
            case WallNutState.OK:
                HPUpdate(2680);
                break;
            case WallNutState.NotGood:
                HPUpdate(1360);
                break;
            default: break;
        }
    }
    public void HPUpdate(float value)
    {
        if (Hp <= value)
        {
            if (state == WallNutState.OK)
            {
                animator.SetTrigger("NotGood");
            }
            if (state == WallNutState.NotGood)
            {
                animator.SetTrigger("Bad");
            }
            state = state + 1;
        }
    }

}
