using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    //单例
    public static HandManager Instance { get; private set; }
    //所有植物预制体
    public List<Plant> plantPrefabs = new List<Plant>();
    //当前植物预制体
    private Plant currentPlant;
    //当前卡片（为获取改植物所需要的阳光值
    private Card currentCard;

    public GameObject StaticImage;
    public BigCell BigCell;

    private void Awake()
    {
        currentCard = null;
        currentPlant = null;
    }
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        FollowCursor();
    }

    public bool AddPlant(PlantType plantType, Card card)
    {//实例化预制体
        if (currentPlant == null || currentCard == null)
        {
            Plant plantPrefab = GetPlant(plantType);
            if (plantPrefab != null)
            {
                
                currentCard = card;
                currentPlant = GameObject.Instantiate(plantPrefab);
                StaticImage = currentPlant.gameObject.transform.GetChild(0).gameObject;
                StaticImage.transform.parent = null;
                return true;

            }
        }
        return false;

    }
    public Plant GetPlant(PlantType plantType)
    {//根据植物类型检测是否有预制体
        foreach (Plant plant in plantPrefabs)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        return null;
    }
    public void SetPlant(Cell cell)
    {//种植
        if (currentPlant == null)
        {
            return;
        }
        else
        {
            if (cell.HadPlant == false)
            {
                //销毁半透明植物
                Destroy(StaticImage.gameObject);
                StaticImage = null;

                cell.HadPlant = true;
                currentPlant.TransitionToEnable();
                //销毁用于判断植物位置的碰撞器
                Destroy(currentPlant.GetComponent<BoxCollider2D>());
                //种植
                currentPlant.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, -3);
                currentPlant.Cell = cell;
                BigCell.Plant2D = null;
                
                //扣除阳光
                SunManager.Instance.SubSun(currentCard.needSunPoint);
                //卡片进入冷却
                currentCard.TransitionToCooling();
                currentPlant = null;
                currentCard = null;

                AudioControl.Instance.PlayClip(Config.plant);
            }


        }

    }
    public void CancelPlant()
    {//取消种植
        if (currentPlant == null)
        {
            return;
        }
        else
        {
            Destroy(StaticImage.gameObject);
            StaticImage = null;
            Destroy(currentPlant.gameObject);
            currentCard.TransitionToReady();
            currentPlant = null;
            currentCard = null;

        }
    }
    void FollowCursor()
    {//当前植物跟随光标
        if (currentPlant != null)
        {
            Vector3 currentCursorPos = Input.mousePosition;
            Vector3 currentWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(currentCursorPos.x, currentCursorPos.y, 9));
            currentPlant.gameObject.transform.position = currentWorldPos;
        }
    }

    public bool JudgeCurrentNull()
    {//判断当前选中卡片和植物是否为空
        if (currentPlant == null || currentCard == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
