using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public Sections[] Section;
    public Abilities[] Ability;
    Manager manager;
    [SerializeField]
    public Text Upgrade;

    [SerializeField]
    public Text Name;

    [SerializeField]
    public Text Description;

    // Start is called before the first frame update
    void Start()
    {
        Section = FindObjectsOfType<Sections>();
        Ability = FindObjectsOfType<Abilities>();
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ability != manager.Ability)
        {
            Ability = manager.Ability;
        }
        foreach(Abilities a in Ability)
        {
            if(a.name != Name.text && a.Active == 1)
            {
                a.Active = 0;
            }
        }
    }

    public void Upgrade_Ability()
    {
        foreach(Abilities a in Ability)
        {
            if(a.Active == 1 && manager.Money >= a.Cost)
            {
                a.Upgrade();
                manager.Money -= a.Cost;
            }
        }
    }
}
