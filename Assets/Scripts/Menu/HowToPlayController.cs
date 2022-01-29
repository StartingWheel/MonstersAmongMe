using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayController : MonoBehaviour
{
    [SerializeField] private ButtonsController _bc;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bc.HowToPlayOff();
        }
    }
}
