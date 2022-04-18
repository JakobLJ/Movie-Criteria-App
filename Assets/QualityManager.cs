using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityManager : MonoBehaviour
{
    
    // 1=1 1.5=2 2=3 2.5=4 3=5 3.5=6 4=7 4.5=8 5=9, this is the conversion chart for how the in-app score should translate to the api score. A mathematic formula for this would be x=y*2-1, where y is the in-app score and x is the api score
    //All methods here are tied to buttons
    public void QualityUp()
    {
        if (float.Parse(GameObject.Find("x out of").GetComponent<Text>().text) != 5)
        {
       GameObject.Find("x out of").GetComponent<Text>().text = Convert.ToString(float.Parse(GameObject.Find("x out of").GetComponent<Text>().text) + 0.5f);
        }
    }
    public void QualityDown()
    {
        if (float.Parse(GameObject.Find("x out of").GetComponent<Text>().text) != 1)
        {
       GameObject.Find("x out of").GetComponent<Text>().text = Convert.ToString(float.Parse(GameObject.Find("x out of").GetComponent<Text>().text) - 0.5f);
        }
    }
    public void AddQualityCriteria()
    {
        GameObject.Find("Criteria").GetComponent<CriteriaCode>().minQuality = Convert.ToString(float.Parse(GameObject.Find("x out of").GetComponent<Text>().text)*2f-1f);
    }

}
