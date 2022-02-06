using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    Manager manager;

    Events events;

    [SerializeField]
    public int Extra_Popularity;
    [SerializeField]
    public int Less_Opposition;
    [SerializeField]
    int Extra_Money;

    public int Active = 0;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        events = FindObjectOfType<Events>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        if (this.gameObject.name != "Protest")
        {
            foreach (Sections s in manager.Section)
            {
                s.Support_Percentage += Extra_Popularity;
                s.Opposition_Percentage -= Less_Opposition;
            }
            manager.Increase_Money += Extra_Money;
            events.Active_Event = this.gameObject;
        }
        /*else
        {
            events.Protest_Active = this.gameObject;
        }*/
        Active = 1;
    }
}
