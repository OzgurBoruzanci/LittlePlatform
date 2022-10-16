using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsScript : MonoBehaviour
{

    GameObject Canvas; /*Level2, Level3, Level4, Level5, Level6;*/
    int recordint;
    private void Start()
    {
        recordint = PlayerPrefs.GetInt("Record");
        Canvas = GameObject.Find("CanvasLevels");
        //Level3 = GameObject.Find("Level 3");
        //Level4 = GameObject.Find("Level 4");
        //Level5 = GameObject.Find("Level 5");
        //Level6 = GameObject.Find("Level 6");

        //Level2.GetComponent<Button>().interactable = false;
        //Level3.GetComponent<Button>().interactable = false;
        //Level4.GetComponent<Button>().interactable = false;
        //Level5.GetComponent<Button>().interactable = false;
        //Level6.GetComponent<Button>().interactable = false;
        
        

        for (int i = recordint+2; i < Canvas.transform.childCount; i++)
        {
            Canvas.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }


    }
    public void BetweenLevels()
    {
        SceneManager.LoadScene("Levels");
    }
    public void GoLevel1()
    {
        SceneManager.LoadScene("1");
    }
    public void GoLevel2()
    {
        SceneManager.LoadScene("2");
    }
    public void GoLevel3()
    {
        SceneManager.LoadScene("3");
    }
    public void GoLevel4()
    {
        SceneManager.LoadScene("4");
    }
    public void GoLevel5()
    {
        SceneManager.LoadScene("5");
    }
    public void GoLevel6()
    {
        SceneManager.LoadScene("6");
    }
    public void Play()
    {
        if (recordint==0)
        {
            SceneManager.LoadScene("1");
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Record"));
            //Debug.Log("kayıtlı");
        }
        
    }
    public void Exit()
    {
        Application.Quit();
    }

}
