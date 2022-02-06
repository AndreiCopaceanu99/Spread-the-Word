using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [SerializeField]
    GameObject[] Panel;

    [SerializeField]
    InputField Name_Text;
    static string Name;

    public string Keep_Name;
    // Start is called before the first frame update
    void Start()
    {
        Panel[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Keep_Name = Name;
    }

    public void Update_Name()
    {
        Name = Name_Text.text;
        Debug.Log(Name);
    }

    public void Play()
    {
        Panel[0].SetActive(false);
        Panel[1].SetActive(true);
    }

    public void Start_Game()
    {
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        Panel[1].SetActive(false);
        Panel[0].SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
