using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioSource effect;

    public GameObject Bg;
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject creditMenu;

    private void Start()
    {
        Bg.SetActive(true);
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        creditMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    public void play()
    {

        SceneManager.LoadScene("TestingGround");

    }

    public void BackMenu()
    {

        SceneManager.LoadScene("Main Menu");

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
        Application.Quit();
    }

    public void playEffect()
    {
        effect.Play(0);
    }
}
