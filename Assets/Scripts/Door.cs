using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Outline _outline;
    [SerializeField] private int outlineWidth = 10;//толщина обводки 

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (!ol)
            {
                Debug.Log("Door Enter");
            }
        }
    }

    private bool ol;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
        ol = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (ol)
        {
            if(_player.keys == 2)
            {
                ol = false;
                _outline.OutlineWidth = outlineWidth;
            }
        }
    }
}
