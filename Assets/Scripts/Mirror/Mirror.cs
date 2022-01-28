using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private Material glass;
    [SerializeField] private Material nearPlayerMater;
    [SerializeField] private float distancePlayer = 5;
    [SerializeField] private GameObject mirrorGlass;
    private bool _isPlayerNear;

    private RaycastHit hit;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            if (!other.GetComponent<Player>().isImba)
            {
                other.GetComponent<Player>().BecomeImba();
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _isPlayerNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.SphereCast(new Ray(transform.position, transform.forward), 0.75f, out hit))
        {
            if (_isPlayerNear)
            {
                if (!hit.transform.gameObject.GetComponent<Player>() || hit.distance > distancePlayer)
                {
                    mirrorGlass.GetComponent<Renderer>().sharedMaterial = glass;
                    _isPlayerNear = false;

                }
            } else
            {
                if (hit.transform.gameObject.GetComponent<Player>() && hit.distance < distancePlayer)
                {
                    mirrorGlass.GetComponent<Renderer>().sharedMaterial = nearPlayerMater;
                    _isPlayerNear = true;
                }
            }
        }
    }
}
