using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Sections[] Section;
    public long Population = 0;
    public long Support = 0;
    public long Opposition = 0;
    [SerializeField]
    Text Timer;
    public float Speed = 0;

    [SerializeField]
    public int Change_Week;

    public int Week = 0;

    public int Start_Game = 0;

    [SerializeField]
    GameObject Upgrade_Panel;
    [SerializeField]
    GameObject Main_UI;
    [SerializeField]
    Text[] Money_Text;

    public Abilities[] Ability;

    Upgrades Upgrade_Menu;

    public long Money;
    public long Increase_Money = 1;

    [SerializeField]
    public GameObject[] Instructions;

    public string Name_Text = "NO TO RACISM";

    [SerializeField]
    GameObject Menu;

    [SerializeField]
    GameObject[] Win_Lose;

    Main_Menu Main;
    [SerializeField]
    Text Name;

    Events events;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        events = FindObjectOfType<Events>();
        Section = FindObjectsOfType<Sections>();
        Ability = FindObjectsOfType<Abilities>();
        Upgrade_Menu = FindObjectOfType<Upgrades>();
        Upgrade_Panel.SetActive(false);

        foreach (GameObject g in Instructions)
        {
            g.SetActive(false);
        }
        Instructions[0].SetActive(true);

        Menu.SetActive(false);

        foreach(GameObject g in Win_Lose)
        {
            g.SetActive(false);
        }

        Main = GetComponent<Main_Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        Name_Text = Main.Keep_Name;
        Name.text = Name_Text;

        if (Change_Week == 5)
        {
            Change_The_Time();
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Menu.SetActive(true);
            Time.timeScale = 0;
        }

        if(Support >= Population && Start_Game != 0)
        {
            Win();
        }
        if (Support <= 0 && Start_Game != 0)
        {
            Lose();
        }

        Possitive_Events();
    }

    void Possitive_Events()
    {
        foreach(Abilities a in Ability)
        {
            if(a.Active == 2)
            {
                if(a.name == "Twitter" && Support * 100 / Population >= 1 && events.Possitive_Events[0].Active == 0)
                {
                    events.Possitive_Events[0].gameObject.SetActive(true);
                    events.Possitive_Events[0].Upgrade();
                }
                if (a.name == "Facebook" && Support * 100 / Population >= 5 && events.Possitive_Events[1].Active == 0)
                {
                    events.Possitive_Events[1].gameObject.SetActive(true);
                    events.Possitive_Events[1].Upgrade();
                }
                if (a.name == "YouTube" && Support * 100 / Population >= 10 && events.Possitive_Events[2].Active == 0)
                {
                    events.Possitive_Events[2].gameObject.SetActive(true);
                    events.Possitive_Events[2].Upgrade();
                }
            }
        }
    }

    void Change_The_Time()
    {
        Speed += Time.deltaTime;
        if(Speed >= Change_Week)
        {
            Week++;
            Update_World();
            Money += Increase_Money;
            Money_Text[0].text = "$ " + Money.ToString();
            Speed = 0;
            Money_Text[1].text = "$ " + Money.ToString();
        }
        foreach(Sections s in Section)
        {
            s.Update_Stats();
        }
        Timer.text = "Week: " + Week.ToString();
    }

    public void Pause()
    {
        if (Start_Game != 0)
        {
            Time.timeScale = 0;
        }
    }

    public void Normal()
    {
        if (Start_Game != 0)
        {
            Time.timeScale = 1;
        }
    }

    public void Fast()
    {
        if (Start_Game != 0)
        {
            Time.timeScale = 5;
        }
    }

    public void Update_World()
    {
        Population = 0;
        Support = 0;
        Opposition = 0;

        foreach (Sections s in Section)
        {
            if (s.name != "World Map")
            {
                Population += s.Population;
                Support += s.Support;
                Opposition += s.Opposition;
            }
        }

        Increase_Money += Support / 100000;
    }

    public void Increase_Popularity()
    {
        foreach(Sections s in Section)
        {
            s.Support_Percentage += 1000;
        }
    }

    public void Increase_Opposition()
    {
        foreach (Sections s in Section)
        {
            s.Opposition_Percentage += 1000;
        }
    }

    public void Upgrade()
    {
        Upgrade_Panel.SetActive(true);
        Main_UI.SetActive(false);
        Money_Text[1].text = "$ " + Money.ToString();
        Change_Week = 0;

    }

    public void Close_Upgrade()
    {
        foreach (Abilities a in Ability)
        {
            if (a.Active == 1)
            {
                a.Active = 0;
            }
        }
        Upgrade_Menu.Upgrade.text = "Upgrade";
        Upgrade_Panel.SetActive(false);
        Main_UI.SetActive(true);
        Change_Week = 5;
    }

    public void Extend()
    {
        foreach(Sections s in Section)
        {
            if(s.name != s.Section_Name.text && s.Active != 2)
            {
                s.Active = 0;
            }
            if (Start_Game == 1)
            {
                if (s.Active == 1 && Money >= s.Cost && s.name != "World Map")
                {
                    s.Active = 2;
                    s.Expand();
                }
            }
            else
            {
                if (s.Active == 1 && s.name != "World Map")
                {
                    s.Active = 2;
                    s.Expand();
                }
            }
        }
    }

    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Win()
    {
        Win_Lose[0].SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        Win_Lose[1].SetActive(true);
        Time.timeScale = 0;
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene(0);
    }
}
