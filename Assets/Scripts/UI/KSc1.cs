using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSc1 : MonoBehaviour
{
    [SerializeField] private SceneController _sc;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sc.KSc1Off();
        }
    }
}
