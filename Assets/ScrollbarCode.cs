using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarCode : MonoBehaviour
{
    public float scrollbarSize;
    public int genreNumber;
    public int streamingServiceNumber;
    //Scrollbar needs to shrink by 1/9 for every item over 9
    // Start is called before the first frame update
    void Start()
{
    
}
    void Update()
    {
    }
    IEnumerator ScrollbarMaker()
    {
        //It starts by determining which variable it has to look at.
        if (gameObject.transform.parent.name == "Add Genre Screen")
        {
        scrollbarSize = gameObject.GetComponent<Scrollbar>().size;
        genreNumber = GameObject.Find("GenreManager").GetComponent<GenreManager>().genreNumber;
        // I wanted the size of the scrollbar to be determined by how many elements there were to scroll through, making it long if few and little if many. 9 is the amount of buttons that can be on screen without need for a scrollbar, so for every button over that, it needed to shrink by 1/9 or 0.11f 
        if (genreNumber > 9)
        {
            gameObject.GetComponent<Scrollbar>().size = scrollbarSize - ((genreNumber-9) * 0.11f);
        }
        yield return null;
        }
        if (gameObject.transform.parent.name == "Add Streaming Service Screen")
        {
        scrollbarSize = gameObject.GetComponent<Scrollbar>().size;
        streamingServiceNumber = GameObject.Find("StreamingServiceManager").GetComponent<StreamingServiceManager>().streamingServiceNumber;
        if (streamingServiceNumber > 9)
        {
            gameObject.GetComponent<Scrollbar>().size = scrollbarSize - ((streamingServiceNumber-9) * 0.11f);
        }
        yield return null;
        }
    }

}
