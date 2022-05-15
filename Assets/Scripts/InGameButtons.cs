using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class InGameButtons : MonoBehaviour
{

    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject Restart;
    [SerializeField] private GameObject MainMenu;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OptionPage()
    {
        Options.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartPage()
    {
        Restart.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HomePage()
    {
        MainMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ExitPage()
    {
        Options.SetActive(false);
        Restart.SetActive(false);
        MainMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ainclure");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }




}
