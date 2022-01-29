using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterAI : MonoBehaviour
{
    public float normalSpeed; //обычная скороссть 
    public float runSpeed;    // во время бега

    [SerializeField] private NavMeshAgent agent; //навигация монстра
    [SerializeField] private Player player;   //игрок
    [SerializeField] private Transform playerTransform;   //позиция игрока
    [SerializeField] private float _distance;  //расстояние с которого монстр ловит игрока
    [SerializeField] private float _stunTime;  //время оглушения
    [SerializeField] private List<Vector3> _walkPositions = new List<Vector3>(); //точки направления монстра 

    private Animator myAnimator; //анимация монстра

    private Vector3 _choosenWalkPosition; //выбранное направление
    
    public bool isPursuitMode; //включен ли режим преследования

    public bool isCatch;  //поймал ли игрока 
    public bool getHit; // получил урон

    void Start()
    {
        isCatch = false;
        getHit = false;
        myAnimator = GetComponent<Animator>();
        EndPursuit();
    }

    private void ChooseNewWalkPosition()
    {
        _choosenWalkPosition = _walkPositions[Random.Range(0, _walkPositions.Count)];
    }

    private bool WalkPositionCompleted()
    {
        Vector3 heading = transform.position - _choosenWalkPosition;
        if (heading.sqrMagnitude <= 4)
        {
            return true;
        }
        return false;
    }
    // ======================= Проверка на видимость игрока (в поле зрения) ======================= //
    private bool IsSeePrey()
    {
        RaycastHit hit;
        if (Physics.SphereCast(new Ray(transform.position, transform.forward), 0.20f, out hit))
        {
            if(hit.transform.gameObject.GetComponent<Player>())
            {
                return true;
            }
        }
        return false;
    }
    private bool IsSeeHiddenPrey()
    {
        bool res = false;
        RaycastHit hit;
        if (Physics.SphereCast(new Ray(transform.position, transform.forward), 0.20f, out hit))
        {
            if (hit.transform.gameObject.GetComponent<Player>())
            {
                res =  true;
            } else if (hit.transform.gameObject.GetComponent<HidePlace>())
            {
                if (hit.transform.gameObject.GetComponent<HidePlace>().isUse)
                {
                    res = true;
                }
            }
        }
        return res;
    }
    //==============================================================================================//

    // =============================== Переход в режим преследования ============================== //
    private void StartPursuit()
    {
        isPursuitMode = true; //переход в режим преседования 
        agent.speed = runSpeed; //переход на бег
        myAnimator.Play("Run");
    }
    //==============================================================================================//

    // ================================ Возвращение в обычный режим =============================== //
    private void EndPursuit()
    {
        isPursuitMode = false;
        if (!getHit) myAnimator.Play("Walk");
        agent.speed = normalSpeed; //переход на шаг
        ChooseNewWalkPosition();
        agent.SetDestination(_choosenWalkPosition);
    }
    //==============================================================================================//


    private IEnumerator OnStun()
    {
        myAnimator.Play("Stun");
        agent.enabled = false;
        yield return new WaitForSeconds(_stunTime);
        getHit = false;
        agent.enabled = true;
        myAnimator.Play("Walk");
        ChooseNewWalkPosition();
        agent.SetDestination(_choosenWalkPosition);
    }

    void Update()
    {
        if (!getHit)
        {
            // -------------------- Если преследует игрока -------------------- //
            if (isPursuitMode)
            {
                agent.SetDestination(playerTransform.position);
                Vector3 heading = transform.position - player.transform.position;
                if (heading.sqrMagnitude <= _distance * _distance)
                {
                    if (player.isImba)
                    {
                        getHit = true;
                        StartCoroutine(OnStun());
                    }
                    isCatch = true;
                    EndPursuit();
                }

                if (player.isHidden) //если игрок спрятался
                {
                    EndPursuit();
                }

            }
            else
            // ---------------------- Если не преследует ---------------------- //
            {
                if (WalkPositionCompleted())
                {
                    ChooseNewWalkPosition();
                    agent.SetDestination(_choosenWalkPosition);
                }
                if (IsSeePrey() && !player.isHidden) //Если игрок оказался в поле зрения 
                {
                    StartPursuit(); //Переход в режим преследования
                }

            }
        }
    }
}
