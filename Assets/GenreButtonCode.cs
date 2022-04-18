using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenreButtonCode : MonoBehaviour
{
    public List<string> activeGenres;
    public string buttonGenre;
    public Vector3 startPosition;
    public Vector3 ultimatePosition;
    public float ultimateYPosition;
    public float scrollbarValue;
    void Start()
    {
       startPosition = gameObject.transform.localPosition;
    }
    //This is what happens when you press the button that this component is attached to.
    public void GenreAdd()
    {
    activeGenres = GameObject.Find("Criteria").GetComponent<CriteriaCode>().activeGenres;
    buttonGenre = gameObject.GetComponentInChildren<Text>().text;
    //Here it checks if the genre in question is already in the active list or not. If it isn't (and the button isn't displaying my default error message), it adds the genre on button press, if it is, it removes it.
        if (!activeGenres.Contains(buttonGenre))
        {
          if (buttonGenre != "Error! Try again, please!")
          {
              activeGenres.Add(buttonGenre);
          }
        }
        else
        {
            activeGenres.Remove(buttonGenre);
            //The GetChild(0) here is the tick mark that appears based on whether its on or off
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void Update()
    {
        //This is the code that determines the position of the button, based on the scroll wheel. I made a lot of little finicky adjustments to get it just right, and I could never really do it, but I reached an approximation. 55 is the approximate length everything needs to move down for one new button to move into view. 9 is the number of buttons visible on a single screen.
        ultimateYPosition = startPosition.y + (55*(GameObject.Find("GenreManager").GetComponent<GenreManager>().genreNumber-9));
        ultimatePosition = new Vector3 (startPosition.x, ultimateYPosition, startPosition.z);
        scrollbarValue = GameObject.Find("Scrollbar AGS").GetComponent<Scrollbar>().value;
        //Lerp extrapolates a position based on two Vector3 positions and a percentage. So using the starting position and the ultimate position calculated earlier in update and the percentage value of the scrollbar's position, a scrolling feature is made.
        gameObject.transform.localPosition = Vector3.Lerp(startPosition, ultimatePosition, GameObject.Find("Scrollbar AGS").GetComponent<Scrollbar>().value);
        //If the genre is in the active list, the checkmark is activated. Simple stuff.
        if (activeGenres.Contains(buttonGenre) & !gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
