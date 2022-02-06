using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public int Active = 0;
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Active == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Close_Instruction()
    {
        Active = 1;
        if(name == "Instruction 3")
        {
            manager.Instructions[3].SetActive(true);
        }
        if (name == "Instruction 4")
        {
            manager.Instructions[4].SetActive(true);
        }
    }
}
