using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneControl : MonoBehaviour
{
    public GameObject RegisterWindow;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI InputName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeName_OnClick()
    {
        RegisterWindow.SetActive(true);
    }
    public void Register_OnClick()
    {
        Name.text = InputName.text;
        InputName.text = "";
        RegisterWindow.SetActive(false);
    }

    public void Start_btn_OnClick()
    {  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
