using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SunManager : MonoBehaviour
{
    //����ģʽ
    public static SunManager Instance { get; private set; }
    //���ֵ
    [SerializeField]
    [Header("���ֵ")]
    private uint sunPoint;
    public uint SunPoint
    {
        get { return sunPoint; }
    }
    [SerializeField]
    [Header("���Ԥ����")]
    public Sun SunPrefab;

    [Header("���UI")]
    public TextMeshProUGUI SunPoint_Text;

    [SerializeField]
    [Header("��ʱ")]
    public float SunTime = 5;
    private float sunTimer = 0;


    //���ֵͬ����UI
    public void UpdateSunPointText()
    {
        SunPoint_Text.text = sunPoint.ToString();
    }
    private void Awake()
    {
        //��ʼ��
        Instance = this;
        SunManager.Instance.sunPoint = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��UIֵ
        UpdateSunPointText();
    }

    void Update()
    {
        ProduceSun();
    }

    //�������ֵ ����UI
    public void SubSun(uint needSun)
    {
        sunPoint -= needSun;
        UpdateSunPointText();
    }
    public void AddSun(uint sun)
    {
        sunPoint += sun;
        UpdateSunPointText();
    }
    public void ProduceSun()
    {//ʵ����һ�����
        sunTimer += Time.deltaTime;
        if (sunTimer > SunTime)
        {
            sunTimer = 0;
            SunTime = Random.Range(9, 18);
            Vector3 v3 = new Vector3(Random.Range(-5f, 5.5f), 3.6f, -5);
            Sun sun = GameControl.Instantiate(SunPrefab, v3, transform.rotation);
            Destroy(sun.rb);
            sun.rb = null;

        }
    }

}
