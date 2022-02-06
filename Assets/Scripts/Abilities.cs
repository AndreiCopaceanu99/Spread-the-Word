using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public int Active = 0;
    [SerializeField]
    public int Cost;

    Upgrades Upgrade_Menu;
    Manager manager;

    [SerializeField]
    int Extra_Popularity;
    [SerializeField]
    int Less_Opposition;
    [SerializeField]
    int Extra_Money;

    [SerializeField]
    string Description;

    // Start is called before the first frame update
    void Start()
    {
        Upgrade_Menu = FindObjectOfType<Upgrades>();
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Active == 0)
        {
            Image image = GetComponent<Image>();
            image.color = Color.white;
        }
        if(Active == 1)
        {
            Image image = GetComponent<Image>();
            image.color = Color.grey;
            Upgrade_Menu.Upgrade.text = "Upgrade: $ " + Cost.ToString();
        }
        if(Active == 2)
        {
            Image image = GetComponent<Image>();
            image.color = Color.black;
        }
    }

    public void Update_Cost()
    {
        if (Active == 0)
        {
            Upgrade_Menu.Name.text = this.name;
            Upgrade_Menu.Description.text = Description;
            Active = 1;
        }
        if( Active == 2)
        {
            Upgrade_Menu.Upgrade.text = "Already upgraded";
        }
    }

    public void Upgrade()
    {
        foreach (Sections s in Upgrade_Menu.Section)
        {
            s.Support_Percentage += Extra_Popularity;
            s.Opposition_Percentage -= Less_Opposition;
        }
        manager.Increase_Money += Extra_Money;
        Active = 2;
    }
}
