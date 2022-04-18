using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreamingServiceButtonCode : MonoBehaviour
{
    //All of this is copied from GenreButtonCode. Check the comments there.
     public List<string> activeStreamingServices;
    public string buttonStreamingService;
    public Vector3 startPosition;
    public Vector3 ultimatePosition;
    public float ultimateYPosition;
    public float scrollbarValue;
    void Start()
    {
       startPosition = gameObject.transform.localPosition;
    }

    public void StreamingServiceAdd()
    {
    activeStreamingServices = GameObject.Find("Criteria").GetComponent<CriteriaCode>().activeStreamingServices;
    buttonStreamingService = gameObject.GetComponentInChildren<Text>().text;
        if (!activeStreamingServices.Contains(buttonStreamingService))
        {
          if (buttonStreamingService != "Error! Try again, please!")
          {
              activeStreamingServices.Add(buttonStreamingService);
          }
        }
        else
        {
            activeStreamingServices.Remove(buttonStreamingService);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void Update()
    {
        ultimateYPosition = startPosition.y + (52*(GameObject.Find("StreamingServiceManager").GetComponent<StreamingServiceManager>().streamingServiceNumber-9));
        ultimatePosition = new Vector3 (startPosition.x, ultimateYPosition, startPosition.z);
        scrollbarValue = GameObject.Find("Scrollbar ASSS").GetComponent<Scrollbar>().value;
        gameObject.transform.localPosition = Vector3.Lerp(startPosition, ultimatePosition, GameObject.Find("Scrollbar ASSS").GetComponent<Scrollbar>().value);
        if (activeStreamingServices.Contains(buttonStreamingService) & !gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
