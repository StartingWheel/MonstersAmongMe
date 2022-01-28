using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameGUI : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<MonsterAI> _monsters = new List<MonsterAI>();

    [SerializeField] private TextMeshProUGUI _hiddenText;
    [SerializeField] private TextMeshProUGUI _imbaText;
    [SerializeField] private TextMeshProUGUI _monsterText;
    [SerializeField] private TextMeshProUGUI _doorText;
    [SerializeField] private Image _imbaImage;

    private bool _playerHiddenMode; //последнее состояние игрока в "прятках"
    private bool _playerImbaMode;
    private bool _monstersMode;


    private bool CheckMonsters()
    {
        foreach (var monster in _monsters)
        {
            if (monster.isPursuitMode)
            {
                return true;
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _hiddenText.enabled = false;
        _playerHiddenMode = false;
        _imbaText.enabled = false;
        _playerImbaMode = false;
        _monsterText.enabled = false;
        _monstersMode = false;
        _doorText.enabled = false;

        _imbaImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerHiddenMode) //если игрок был спрятан
        {
            if(!_player.isHidden) //если больше не прячется
            {
                _playerHiddenMode = false;
                _hiddenText.enabled = false;
            }
        } else // если игрок не был спрятан
        {
            if (_player.isHidden) //если спрятался
            {
                _playerHiddenMode = true;
                _hiddenText.enabled = true;
            }
        }

        if (_playerImbaMode) //если игрок в атаке
        {
            if (!_player.isImba) //если больше не атака
            {
                _imbaImage.enabled = false;
                _playerImbaMode = false;
                _imbaText.enabled = false;
            }
        }
        else // если игрок не в атаке
        {
            if (_player.isImba) //если в атаке
            {
                _imbaImage.enabled = true;
                _playerImbaMode = true;
                _imbaText.enabled = true;
            }
        }

        if (_monstersMode)
        {
            if (!CheckMonsters() || _player.isImba) 
            {
                _monstersMode = false;
                _monsterText.enabled = false;
            }
        }
        else 
        {
            if (CheckMonsters() && !_player.isImba) 
            {
                _monstersMode = true;
                _monsterText.enabled = true;
            }
        }

        if(_player.keys == 2)
        {
            _doorText.enabled = true;
        }
    }
}
