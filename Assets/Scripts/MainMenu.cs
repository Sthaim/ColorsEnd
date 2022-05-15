using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject creditMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

    // Start is called before the first frame update
    public void play()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName:"");
    }

    public void option()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
        creditMenu.SetActive(false);
    }

    public void credit()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(false);
        creditMenu.SetActive(true);
    }

    public void back()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        
    }
}
