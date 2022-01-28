using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameButton : MonoBehaviour
{
    [SerializeField] private SceneController _sc;

    public void Yes()
    {
        Debug.Log("djk");
        Application.Quit();
    }

    public void No()
    {
        _sc.ExitOff();
    }
}
