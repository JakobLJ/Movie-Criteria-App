using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CriteriaCode : MonoBehaviour
{
    //This is the base url for sending GET requests that I'll use for my film recommendations. Any other criteria can be added to the request simply by adding more stuff to the url, which is basically what the program does.
    public string criteriaHTML = "https://api.themoviedb.org/3/discover/movie?api_key=8dbc571fbec87fe32e10d4b0f780affb&language=en-US&region=DK&sort_by=popularity.desc&include_adult=true&include_video=true&page=1&watch_region=DK&with_watch_monetization_types=free";

    //false means they have access to no streaming services, true means they do. anything relating to monetization should be asked before the whole thing starts.
    public bool hasStreamingServices = false;
    public bool okayWithAds = false;
    public bool okayWithRenting = false;
    public bool okayWithBuying = false;
    public List<string> activeGenres;
    public List<string> activeStreamingServices;
    public int maxRunTime;
    public int minRunTime;
    public int runtimeApprox;
    //era should be like 1980 for the 80s 1990 for the 90s etc etc
    public List<string> activeEras;
    public string minQuality;
    public string resultsUnprocessed;
    string[] seperators = { "\"title\":\"", "\",\"video"};
    public string finalResultTitle;
    



    // Update is called once per frame
    void Update()
    {
    

    }
    public void StartHTMLDefiner()
    {
        StartCoroutine(HTMLDefiner());

    }
    //This is where all the stuff is added to the url, pretty simple in the beginning
    public IEnumerator HTMLDefiner()
    {
        if (hasStreamingServices == true)
        {
            criteriaHTML += "&with_watch_monetization_types=flatrate";
        }
        if (okayWithAds == true)
        {
            criteriaHTML += "&with_watch_monetization_types=ads";
        }
        if (okayWithRenting == true)
        {
            criteriaHTML += "&with_watch_monetization_types=rent";
        }
        if (okayWithBuying == true)
        {
            criteriaHTML += "&with_watch_monetization_types=buy";
        }
        if (activeGenres.Count > 0)
        {
            criteriaHTML += "&with_genres=";
        }
        foreach (string genre in activeGenres)
        {
            criteriaHTML += (GameObject.Find("GenreManager").GetComponent<GenreManager>().genreObjects.Find(x => x.name == genre).id + ", ");
        }
        criteriaHTML.Remove(criteriaHTML.LastIndexOf(", "));
        if (activeStreamingServices.Count > 0)
        {
            criteriaHTML += "&with_watch_providers=";
        }
        //The database api needs the ID of the streaming services, rather than the names. Therefore, I need to match all the names of the streaming services, as they're listed in the activeStreamingServices list and find the id for them. I don't fully understand the lamba expression stuff, but used it since that's how they do it in the official c# documentation on the find command
        foreach (string streamService in activeStreamingServices)
        {
            criteriaHTML += (GameObject.Find("StreamingServiceManager").GetComponent<StreamingServiceManager>().streamingServiceObjects.Find(x => x.name == streamService).id + ", ");
        }
        criteriaHTML.Remove(criteriaHTML.LastIndexOf(", "));
        //I decided to split release date into decades for this first draft. I think it's pretty common to hear people say they like 80s cinema and stuff, but not very common to hear people say they specifically enjoy films made in 1986, so as far as what I'm going for here, this seemed like the best way to go at it.
        foreach (string era in activeEras)
        {
            criteriaHTML += ("&primary_release_date.gte=" + era + "-01-01&primary_release_date.lte=" + era.Remove(3, 1) + "9-12-31");
        }
        if (maxRunTime != 0)
        {
            criteriaHTML += "&with_runtime.lte=" + maxRunTime;
        }
        if (minRunTime != 0)
        {
            criteriaHTML += "&with_runtime.gte=" + minRunTime;
        }
        if (runtimeApprox != 0)
        {
            criteriaHTML += "&with_runtime.lte=" + (runtimeApprox+10);
            criteriaHTML += "&with_runtime.gte=" + (runtimeApprox-10);
        }
        if (minQuality != "")
        {
            criteriaHTML += "&vote_average.gte=" + minQuality;
        }
        StartCoroutine(GenerateFinalResult());
    yield return null;
    }
    //These three are used by the buttons in the streaming service settings section. They simply set the option to the opposite of what it is.
    public void ToggleAdbased()
    {
        okayWithAds = !okayWithAds;
    }
    public void ToggleRenting()
    {
        okayWithRenting = !okayWithRenting;
    }
    public void ToggleBuying()
    {
        okayWithBuying = !okayWithBuying;
    }
    IEnumerator GenerateFinalResult()
    {
       UnityWebRequest request = UnityWebRequest.Get(criteriaHTML);
       yield return request.SendWebRequest();

       if (request.responseCode != 200)
       {
           Debug.Log(request.error);
       }
       else
       {
           resultsUnprocessed = request.downloadHandler.text;
           finalResultTitle = resultsUnprocessed.Split(seperators, 0)[1];
           GameObject.Find("Suggestion Text").GetComponent<Text>().text = finalResultTitle;
           Debug.Log(finalResultTitle);
           Debug.Log(criteriaHTML);
           criteriaHTML = "https://api.themoviedb.org/3/discover/movie?api_key=8dbc571fbec87fe32e10d4b0f780affb&language=en-US&region=DK&sort_by=popularity.desc&include_adult=true&include_video=true&page=1&watch_region=DK&with_watch_monetization_types=free";
       }
    }
}
