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
    [SerializeField] private GameObject _exit;
    [SerializeField] private GameObject _end1;
    [SerializeField] private GameObject _end2;
    [SerializeField] private GameObject _end3;
    [SerializeField] private GameObject _loose;
    [SerializeField] private GameObject _avtors;

    [SerializeField] private GameObject _kSc1;
    [SerializeField] private GameObject _kSc2;

    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _mall;

    [SerializeField] private GameObject _mons1;
    [SerializeField] private GameObject _mons2;

    private bool _isMap;
    private bool _isExit;
    private bool _isEnd1;
    private bool _isEnd2;


    void Start()
    {
        Time.timeScale = 1;
        _map.SetActive(false);
        _hint.SetActive(false);
        _hint2.SetActive(false);
        _exit.SetActive(false);
        _end1.SetActive(false);
        _end2.SetActive(false);
        _end3.SetActive(false);
        _loose.SetActive(false);
        _avtors.SetActive(false);
        _kSc2.SetActive(false);

        _isMap = false;
        _isExit = false;
        _isEnd1 = false;
        _isEnd2 = false;
        KSc1On();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !_isMap)
        {
            MapOn();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !_isExit)
        {
            ExitOn();
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

    private void ExitOn()
    {
        _isExit = true;
        _exit.SetActive(true);
        PauseOn();
    }

    public void ExitOff()
    {
        _isExit = false;
        _exit.SetActive(false);
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

    public void End1On()
    {

        _end1.SetActive(true);
        _mall.Stop();
        _music.Play();
        _isEnd1 = true;
        Destroy(_mons1);
        Destroy(_mons2);
        _player.BeCatched();
        PauseOn();
    }
    public void EndOff()
    {
        if (_isEnd1)
        {
            _end1.SetActive(false);
        } else if (_isEnd2)
        {
            _end2.SetActive(false);
        } else
        {
            _end3.SetActive(false);
        }
        _avtors.SetActive(true);
    }

    public void End2On()
    {
        _end2.SetActive(true);
        _mall.Stop();
        _music.Play();
        _isEnd2 = true;
        _player.BeCatched();
        PauseOn();
    }

    public void KSc1On()
    {
        _kSc1.SetActive(true);
        PauseOn();
    }

    public void KSc1Off()
    {
        _kSc1.SetActive(false);
        PauseOff();
    }

    public void KSc2On()
    {
        _kSc2.SetActive(true);
        PauseOn();
    }

    public void KSc2Off()
    {
        _kSc2.SetActive(false);
        PauseOff();
    }


    public void End3On()
    {
        _end3.SetActive(true);
        _mall.Stop();
        _music.Play();
        _player.BeCatched();
        PauseOn();
    }

    public void LooseOn()
    {
        _loose.SetActive(true);
        PauseOn();
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
