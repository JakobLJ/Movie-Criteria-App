using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPTester : MonoBehaviour
{
    //I just use this method to test API requests.
    public string requestURL = "https://api.themoviedb.org/3/watch/providers/movie?api_key=8dbc571fbec87fe32e10d4b0f780affb&language=en-US&watch_region=DK";
     void Start()
    {
    CoroutineStarter();
    }
    void CoroutineStarter()
    {
        Debug.Log("coroutinestarter started");
        StartCoroutine(GetRequest());
    }
    IEnumerator GetRequest()
    {
       UnityWebRequest request = UnityWebRequest.Get(requestURL);
       yield return request.SendWebRequest();

       if (request.responseCode != 200)
       {
           Debug.Log(request.error);
       }
       else
       {
           Debug.Log(request.downloadHandler.text);
       }
    }
}
