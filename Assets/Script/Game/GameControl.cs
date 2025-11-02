using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    Ready,
    Playing,
    LastAttack,
    GameOver


}
public class GameControl : MonoBehaviour
{
    //����
    public static GameControl Instance { get; private set; }



    //�Ϸ�UI��������ʼ������
    public UIControl UIControl;

    //���
    public CameraControl CameraControl;

    //��Ϸ״̬
    public GameState GameState { get; set; }
    private void Awake()
    {
        Instance = this;
        GameState = GameState.Ready;
    }

    private void Start()
    {
        ZombiePro.Instance.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        switch (GameState)
        {
            case GameState.Ready:
                ReadyUpdate();
                break;
            case GameState.Playing:
                PlayingUpdate();
                break;
            case GameState.LastAttack:
                LastAttackUpdate();
                break;
            default:
                break;
        }

    }
    public void ReadyUpdate()
    {
        if (UIControl.startOver == true)
        {
            TransitionToPlaying();
        }
    }
    public void TransitionToPlaying()
    {
        ZombiePro.Instance.enabled = true;
        GameState = GameState.Playing;
        AudioControl.Instance.PlayBgm(Config.bgm1);
    }
    private void PlayingUpdate()
    {
    }

    public void TransitionToLastAttack()
    {
        if (GameState != GameState.LastAttack)
        {
            GameState = GameState.LastAttack;
        AudioControl.Instance.PlayClip(Config.lastAttack,0.8f);
        }

    }
    public void LastAttackUpdate()
    {
            if (ZombiePro.Instance.ZombieCount == ZombiePro.Instance.ZombieProCount)
            {
                foreach (var item in ZombiePro.Instance.ZombieAttackList)
                {
                    if (item != null)
                    {
                        return;
                    }
                    Win();
                }
            }
    }
    public void Win()
    {
        SunManager.Instance.SunTime = 99999;
        GameState = GameState.GameOver;
        UIControl.GameWin();
        AudioControl.Instance.PlayClip(Config.win_Music);
        AudioControl.Instance.Stop();

    }
    public void Fail()
    {
        GameState = GameState.GameOver;
        //��ֹ��Ƭ��ȴ
        UIControl.animator.SetTrigger("UIOver");

        //��ֹ��ʬ��Ϊ
        ZombiePro.Instance.ProTime = 999;
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach (var plant in plants)
        {
            plant.GetComponent<Animator>().enabled = false;
            plant.GetComponent<Plant>().enabled = false;
        }
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (var zombie in zombies)
        {
            zombie.GetComponent<Animator>().enabled = false;
            zombie.GetComponent<Zombie>().enabled = false;
        }
        CameraControl.animator.SetTrigger("CameraFail");

        //��ֹ�������
        SunManager.Instance.SunTime = 999;
    }
    public void Tryagain_OnClick()
    {//���¿�ʼ
        AudioControl.Instance.PlayClip(Config.btn_Click1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
