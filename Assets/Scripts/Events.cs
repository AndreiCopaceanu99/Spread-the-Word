using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    [SerializeField]
    public Event[] Possitive_Events;

    [SerializeField]
    public Event Protest;

    [SerializeField]
    public Event[] Negative_Events;

    public GameObject Active_Event;

    public GameObject Protest_Active;

    Manager manager;

    public Sections active_section;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();

        foreach(Event g in Possitive_Events)
        {
            g.gameObject.SetActive(false);
        }

        foreach (Event g in Negative_Events)
        {
            g.gameObject.SetActive(false);
        }

        Protest.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close_Event()
    {
        Active_Event.SetActive(false);
    }

    public void Aggressively()
    {
        active_section.Support_Percentage -= 100;
        active_section.Opposition_Percentage += 100;
        //Protest_Active.SetActive(false);
        Protest.gameObject.SetActive(false);
        Protest.Active = 0;
    }

    public void Peacefully()
    {
        active_section.Support_Percentage += 100;
        active_section.Opposition_Percentage -= 100;
        //Protest_Active.SetActive(false);
        Protest.gameObject.SetActive(false);
        Protest.Active = 0;
    }
}
