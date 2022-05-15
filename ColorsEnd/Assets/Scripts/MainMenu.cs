using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioSource effect;
    public AudioSource effectBack;
    public AudioSource transition;

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
        transition.Play(0);
    }

    public void option()
    {
        effect.Play(0);
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
        creditMenu.SetActive(false);
    }

    public void credit()
    {
        effect.Play(0);
        mainMenu.SetActive(false);
        optionMenu.SetActive(false);
        creditMenu.SetActive(true);
    }

    public void back()
    {
        effectBack.Play(0);
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

    public void testSond()
    {
        effectBack.Play(0);
    }

    public void OnApplicationQuit()
    {
        
    }
}
