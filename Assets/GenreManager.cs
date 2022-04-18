using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GenreManager : MonoBehaviour
{
    public string genreRequestURL = "https://api.themoviedb.org/3/genre/movie/list?api_key=8dbc571fbec87fe32e10d4b0f780affb&language=en-US";
    public string genreListUnprocessed = "empty";
    public List<string> genres;
    string[] seperators = { "\"name\":\"", "\"}"};
    public GameObject GenreScreen;
    public GameObject buttonToInstantiate;
    public int genreNumber = 0;
    public Transform parent;
    public class GenreObject
    {
        public string name;
        public string id;
        public GenreObject(string objectName, string objectID)
        {
            name = objectName;
            id = objectID;
        }
    }
    GenreObject genreObject;
    public string genreObjectName;
    public string genreObjectID;
    public List<GenreObject> genreObjects = new List<GenreObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateGenreList());
        StartCoroutine(GenreButtonGenerator());
        StartCoroutine(GenreObjectGenerator());
    }

 // This coroutine gets the API's list of genres, unless if there's an error. It then downloads the list and splits it up into a bunch of little strings containing a genre each.
    IEnumerator GenerateGenreList()
    {
       UnityWebRequest request = UnityWebRequest.Get(genreRequestURL);
       yield return request.SendWebRequest();

       if (request.responseCode != 200)
       {
           Debug.Log(request.error);
       }
       else
       {
           genreListUnprocessed = request.downloadHandler.text;
           foreach (string genre in genreListUnprocessed.Split(seperators, 0))
           {
               if (!genre.Contains("\"id\"") & !genre.Contains("}"))
               {
                   genres.Add(genre);
               }
           }
       }
    }
    //This coroutine then generates a bunch of buttons for each genre.
    IEnumerator GenreButtonGenerator()
    {
        while (!GenreScreen.activeInHierarchy)
        {
            yield return new WaitForSeconds(.1f)    ;
        }
        yield return new WaitForSeconds(.50f);
            //The first genre in the list is put on the already existing button that will serve as the blueprint for all the rest.
            Text iniButtonText = buttonToInstantiate.GetComponentInChildren<Text>();
            iniButtonText.text = genres[0];
            foreach (string genre in genres)
            {
                //Because the genre at index 0 is already on a button it's skipped over here.
                if (genre != genres[0])
                {
                //All the new genrebuttons get their own number, so it's easier to tell the difference between them in the scene.
                genreNumber++;
                //Each new button needs to appear 50 units of distance beneath the last one, so to determine how far down it needs to appear from the original, I multiply that with the number it has just been assigned.
                GameObject instantiatedObject = Instantiate(buttonToInstantiate, new Vector3(buttonToInstantiate.transform.localPosition.x, (buttonToInstantiate.transform.localPosition.y-(genreNumber*50)), 0f), Quaternion.identity);
                instantiatedObject.name = "Instantiated Button " + genreNumber;
                //The parent in question is defined in the editor. I always want the parent of the new instantiated objects to be the same as the original, so their positions align.
                instantiatedObject.transform.SetParent(parent, false);
                //And here the text is added to the button.
                GameObject.Find("Instantiated Button " + genreNumber).GetComponentInChildren<Text>().text = genres[genreNumber];
                }
            //Since I want the back button to always appear on top, it has to be the last sibling. That's just how it works.
            GameObject.Find("Back Button AGS").transform.SetAsLastSibling();
            }
        //Now that all the buttons are ready, the scrollbarmaker method has everything it needs to do its thing.
         GameObject.Find("Scrollbar AGS").SendMessage("ScrollbarMaker");
    }
    //Copied from streamingservicemanager, made for same reason
    IEnumerator GenreObjectGenerator()
    {
        yield return new WaitForSeconds (0.1f);
        string[] objectSeperators = {"{","}"};
        foreach (string almostObject in genreListUnprocessed.Split(objectSeperators, 0))
        {
            //In streamingservicemanager i used the letter o to see if a string as worth looking at, since theres only two things I need to keep track of for this object, I'll just put in both the relevant factors
            if (almostObject.Contains("id")|almostObject.Contains("name"))
            {
            //This cleans up the object and fits it into predefined variables.
            foreach (string objectSplit in almostObject.Split(','))
            {
                if (objectSplit.Contains("name"))
                {
                    genreObjectName = objectSplit.Replace("name", "").Replace("\"", "").Replace(":", "").Trim();
                }
                if (objectSplit.Contains("id"))
                {
                   genreObjectID = objectSplit.Replace("id", "").Replace("\"", "").Replace(":", "").Trim();
                }
            }
            //And this writes it to the variable object genreObject and adds it to the list
            genreObject = new GenreObject(genreObjectName, genreObjectID);
            genreObjects.Add(genreObject);
            }
        }
    yield return null;
    }
}
