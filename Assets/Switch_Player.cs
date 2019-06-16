using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch_Player : MonoBehaviour
{
    Dropdown m_Dropdown;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(m_Dropdown); });
    }

    // Update is called once per frame
    void Update()
    {
    }

    void DropdownValueChanged(Dropdown change)
    {
        gameObject.transform.parent = GameObject.Find(change.itemText.text).transform;
    }
}