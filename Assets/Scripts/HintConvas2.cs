using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintConvas2 : MonoBehaviour
{
    [SerializeField] SceneController _sceneController;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sceneController.Hint2Off();
        }
    }
}
