using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMonster : MonoBehaviour
{
    [SerializeField] private List<float> speed = new List<float>();    //обычная скороссть 
    [SerializeField] private List<float> runSpeed = new List<float>(); // во время бега
    [SerializeField] private Player player; //игрок 
    [SerializeField] private SceneController _sc;

    [SerializeField] private MonsterAI ai;

    private int _maxHits;

    private int _hitsCount;

    private void Hit()
    {
        Debug.Log("is Hit!");
        _hitsCount++;
        player.ReturnToNormal();
        if (_hitsCount==_maxHits)
        {
            Died();
            return;
        }
        ai.normalSpeed = speed[_hitsCount];
        ai.runSpeed = runSpeed[_hitsCount];
    }

    private void Died()
    {
        _sc.End1On();
    }

    // Start is called before the first frame update
    void Start()
    {
        _maxHits = System.Math.Min(speed.Count, runSpeed.Count);
        ai.normalSpeed = speed[0];
        ai.runSpeed = runSpeed[0];
        _hitsCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.isCatch)
        {
            if (ai.getHit)
            {
                Hit();
                StartCoroutine(player.ExitImba());
            }
            else
            {
                player.Died();
            }
            ai.isCatch = false;
        }
    }
}
