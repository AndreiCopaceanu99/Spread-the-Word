using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sections : MonoBehaviour
{
    [SerializeField]
    public Text Section_Name;
    [SerializeField]
    Text Population_Number;
    [SerializeField]
    Text Support_Number;
    [SerializeField]
    Text Opposition_Number;
    [SerializeField]
    public Text Extend;

    [SerializeField]
    public int Cost;

    public int Active = 0;

    public long Population;
    public long Support = 0;
    public long Opposition = 0;

    int Decrease_Support = 100;
    public int Support_Percentage;
    int Min_Support_Increase;
    int Max_Support_Increase;

    [SerializeField]
    int Increase_Opposition;
    public int Opposition_Percentage;
    int Min_Opposition_Increase;
    int Max_Opposition_Increase;

    int Max_Support = 0;
    int Max_Opposition = 0;

    int Law = 0;

    int Protest = 0;

    int Ban = 0;

    Manager manager;

    Events events;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        events = FindObjectOfType<Events>();
        
        Support_Percentage = 0;
        Min_Support_Increase = 1;
        Max_Support_Increase = 2;
        
        Opposition_Percentage = 0;
        Min_Opposition_Increase = 1;
        Max_Opposition_Increase = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.name == "World Map")
        {
            Population = manager.Population;
            Support = manager.Support;
            Opposition = manager.Opposition;
        }
        else
        {
            if (Opposition <= 0)
            {
                Opposition = 0;
            }
            if (Support <= 0)
            {
                Support = 0;
            }

            if (Support >= Increase_Opposition)
            {
                Opposition_Percentage++;
                Increase_Opposition += Increase_Opposition;
            }

            if(Support>=Population)
            {
                Support = Population;
                Opposition = 0;
                if (Max_Support == 0)
                {
                    manager.Increase_Popularity();
                }
                Max_Support = 1;
            }
            if(Opposition>=Population)
            {

                Opposition = Population;
                Support = 0;

                if (Max_Opposition == 0)
                {
                    manager.Increase_Opposition();
                }
                Max_Opposition = 1;
            }

            if (Support_Percentage > Opposition_Percentage)
            {
                if (Opposition >= Population - Support)
                {
                    Opposition = Population - Support;
                }
            }
            else
            {
                if (Support >= Population - Opposition)
                {
                    Support = Population - Opposition;
                }
            }

            if (Support > 0)
            {
                Image colour = GetComponent<Image>();
                colour.color = new Color(Opposition * 100 / Population * 255 / 100, 255, Support * 100 / Population * 255 / 100, 100);

                if (manager.Week >= Decrease_Support)
                {
                    Support_Percentage--;
                    Decrease_Support += Decrease_Support;
                }
            }

            if (Support * 100 / Population >= 70 && Law == 0)
            {
                events.Possitive_Events[3].gameObject.SetActive(true);
                Text text = events.Possitive_Events[3].GetComponentInChildren<Text>();
                text.text = this.name + " has adopted a law that will recognise and protect your movement. Your popularity will greatly increase everywhere in the world! Support +1000";
                events.Possitive_Events[3].Upgrade();
                Law = 1;
            }

            if (Support * 100 / Population >= 30 && Protest == 0 && Opposition * 100 / Population >= 20 && events.Protest.Active == 0)
            {
                events.Protest.gameObject.SetActive(true);
                Text text = events.Protest.GetComponentInChildren<Text>();
                text.text = "A group of protesters have been attacked in " + this.name + ". How do you want to react?";
                events.Protest.Upgrade();
                events.active_section = this;
                Protest = 1;
            }

            if (Opposition * 100 / Population >= 70 && Ban == 0)
            {
                events.Negative_Events[0].gameObject.SetActive(true);
                Text text = events.Negative_Events[0].GetComponentInChildren<Text>();
                text.text = this.name + " has banned your movement. Your opposition will greatly increase everywhere in the world! Opposition +1000";
                events.Negative_Events[0].Upgrade();
                Ban = 1;
            }
        }
    }

    public void Update_Stats()
    {
        if (Support != 0)
        {
            int r = Random.Range(Min_Support_Increase * Support_Percentage, Max_Support_Increase * Support_Percentage);
            Support += r;
        }
        if (Support * 100 / Population >= 10 || Opposition_Percentage != 0 && Support != 0)
        {
            int r = Random.Range(Min_Opposition_Increase * Opposition_Percentage, Max_Opposition_Increase * Opposition_Percentage);
            Opposition += r;
        }
    }

    public void Button_Pressed()
    {
        if (manager.Start_Game == 0 && this.name != "World Map")
        {
            Extend.text = "Extend: $ 0";
            Section_Name.text = this.name;

            if (Active == 0)
            {
                Active = 1;
            }

            Section_Name.text = this.name;
            Population_Number.text = "Population: " + Population.ToString();
            Support_Number.text = "Support: " + Support.ToString();
            Opposition_Number.text = "Opposition: " + Opposition.ToString();
        }

        if (manager.Start_Game == 1)
        {
            if (this.name == "World Map")
            {
                Population = manager.Population;
                Support = manager.Support;
                Opposition = manager.Opposition;

                Section_Name.text = this.name;
                Population_Number.text = "Population: " + Population.ToString();
                Support_Number.text = "Support: " + Support.ToString();
                Opposition_Number.text = "Opposition: " + Opposition.ToString();
            }
            else
            {

                if (Active == 0)
                {
                    Active = 1;
                }
                Section_Name.text = this.name;
                Population_Number.text = "Population: " + Population.ToString();
                Support_Number.text = "Support: " + Support.ToString();
                Opposition_Number.text = "Opposition: " + Opposition.ToString();

                if (Support == 0)
                {
                    Extend.text = "Extend: $ " + Cost.ToString();
                }
                else
                {
                    Extend.text = "Owned";
                }
            }
        }
    }

    public void Expand()
    {
        if (manager.Start_Game == 0)
        {
            Support = 1;
            Support_Percentage++;
            manager.Update_World();
            manager.Start_Game++;
            manager.Change_Week = 5;
            Time.timeScale = 1;
            manager.Instructions[2].SetActive(true);
        }
        else
        {
            Support = 1;
            manager.Money -= Cost;
        }
    }
}
