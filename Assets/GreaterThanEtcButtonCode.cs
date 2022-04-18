using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreaterThanEtcButtonCode : MonoBehaviour
{
    //I have to return after each one of these or it just cycles through the whole shebang and sets it back to less than
    public void SymbolChange()
    {
        if (GameObject.Find("Greater Than etc button").GetComponentInChildren<Text>().text == "<")
        {
            GameObject.Find("Greater Than etc button").GetComponentInChildren<Text>().text = ">";
            return;
        }
        if (GameObject.Find("Greater Than etc button").GetComponentInChildren<Text>().text == ">")
        {
            GameObject.Find("Greater Than etc button").GetComponentInChildren<Text>().text = "≈";
            return;
        }
        if (GameObject.Find("Greater Than etc button").GetComponentInChildren<Text>().text == "≈")
        {
            GameObject.Find("Greater Than etc button").GetComponentInChildren<Text>().text = "<";
            return;
        }
    }
}
