using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _howToPlay;
    [SerializeField] private GameObject _mainMenu;

    void Start()
    {
        _howToPlay.SetActive(false);
    }
    
    public void Play()
    {
        SceneManager.LoadScene("Playground");
    }

    public void HowToPlay()
    {
        _howToPlay.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void HowToPlayOff()
    {
        _howToPlay.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("djk");
        Application.Quit();
    }
}
