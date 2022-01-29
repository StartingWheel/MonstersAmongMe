using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Door : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Player _player;
    private Outline _outline;
    [SerializeField] private int outlineWidth = 10;//толщина обводки 
    [SerializeField] private SceneController _sc;
    private bool _ol;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_ol)
        {
            _sc.End3On();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (!_ol)
            {
                _sc.End3On();
            }
        }
    }

    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
        _ol = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (_ol)
        {
            if(_player.keys == 2)
            {
                _ol = false;
                _outline.OutlineWidth = outlineWidth;
            }
        }
    }
}
