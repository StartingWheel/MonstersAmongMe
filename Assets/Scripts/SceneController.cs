using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _hint;
    [SerializeField] private GameObject _hint2;

    private bool _isMap;


    void Start()
    {
        _map.SetActive(false);
        _hint.SetActive(false);
        _hint2.SetActive(false);

        _isMap = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !_isMap)
        {
            MapOn();
        }
    } 
    private void MapOn()
    {
        _isMap = true;
        _map.SetActive(true);
        PauseOn();
    }

    public void MapOff()
    {
        _isMap = false;
        _map.SetActive(false);
        PauseOff();
    }

    public void HintOn()
    {
        _hint.SetActive(true);
        PauseOn();
    }

    public void HintOff()
    {
        _hint.SetActive(false);
        PauseOff();
    }

    public void Hint2On()
    {
        _hint2.SetActive(true);
        PauseOn();
    }

    public void Hint2Off()
    {
        _hint2.SetActive(false);
        PauseOff();
    }

    private void PauseOn()
    {
        Time.timeScale = 0;
    }

    private void PauseOff()
    {
        Time.timeScale = 1;
    }
}
