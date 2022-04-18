using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeManager : MonoBehaviour
{
    public int minutes;
    public int hours;
    public Text MinuteCounterText;
    public Text HourCounterText;
    void Start()
    {
       
    }

    void Update()
    {
        if (MinuteCounterText.text != "")
        {
        if (Int32.Parse(MinuteCounterText.text) > 59)
        {
            MinuteCounterText.text = "59";
        }
        }
        
    }
    public void RuntimeCriteriaAdder()
    {
        minutes = Int32.Parse(MinuteCounterText.text);
        hours = Int32.Parse(HourCounterText.text);
        Debug.Log(GameObject.Find("Runtime Greater than text").GetComponent<Text>().text);
        if (GameObject.Find("Runtime Greater than text").GetComponent<Text>().text == "<")
        {
            GameObject.Find("Criteria").GetComponent<CriteriaCode>().maxRunTime = (hours * 60) + minutes; 
        }
        if (GameObject.Find("Runtime Greater than text").GetComponent<Text>().text == ">")
        {
            GameObject.Find("Criteria").GetComponent<CriteriaCode>().minRunTime = (hours * 60) + minutes; 
        }
        if (GameObject.Find("Runtime Greater than text").GetComponent<Text>().text == "≈")
        {
            GameObject.Find("Criteria").GetComponent<CriteriaCode>().runtimeApprox = (hours * 60) + minutes; 
        }
    }
}
