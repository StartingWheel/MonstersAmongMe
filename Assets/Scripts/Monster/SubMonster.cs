using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMonster : MonoBehaviour
{
    [SerializeField] private float speed;    //обычная скороссть 
    [SerializeField] private float runSpeed; // во время бега
    [SerializeField] private Player player; //игрок 
    [SerializeField] private MonsterAI ai;
    [SerializeField] private AudioSource _hitAudio;

    public void OnDestroy()
    {
        _hitAudio.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        ai.normalSpeed = speed;
        ai.runSpeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.isCatch)
        {
            if (ai.getHit)
            {
                player.GetKey();
                _hitAudio.Play();
                StartCoroutine(player.ExitImba());
            }
            else
            {
                player.BeCatched();
            }
            ai.isCatch = false;
        }
    }
}
