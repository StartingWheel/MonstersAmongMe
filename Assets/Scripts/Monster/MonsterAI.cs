using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterAI : MonoBehaviour
{
    public float normalSpeed; //������� ��������� 
    public float runSpeed;    // �� ����� ����

    [SerializeField] private NavMeshAgent agent; //��������� �������
    [SerializeField] private Player player;   //�����
    [SerializeField] private Transform playerTransform;   //������� ������
    [SerializeField] private float _distance;  //���������� � �������� ������ ����� ������
    [SerializeField] private float _stunTime;  //����� ���������

    [SerializeField] private List<Vector3> _walkPositions = new List<Vector3>();
    private Vector3 _choosenWalkPosition;
    
    public bool isPursuitMode; //������� �� ����� �������������

    public bool isCatch;  //������ �� ������ 
    public bool getHit; // ������� ����

    void Start()
    {
        isCatch = false;
        getHit = false;
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
    // ======================= �������� �� ��������� ������ (� ���� ������) ======================= //
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

    // =============================== ������� � ����� ������������� ============================== //
    private void StartPursuit()
    {
        isPursuitMode = true; //������� � ����� ������������ 
        agent.speed = runSpeed; //������� �� ���
    }
    //==============================================================================================//

    // ================================ ����������� � ������� ����� =============================== //
    private void EndPursuit()
    {
        isPursuitMode = false;
        agent.speed = normalSpeed; //������� �� ���
        ChooseNewWalkPosition();
        agent.SetDestination(_choosenWalkPosition);
    }
    //==============================================================================================//


    private IEnumerator OnStun()
    {
        yield return new WaitForSecondsRealtime(_stunTime);
        getHit = false;
    }

    void Update()
    {
        // -------------------- ���� ���������� ������ -------------------- //
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

            if(player.isHidden) //���� ����� ���������
            {
                EndPursuit();
            }

        } else
        // ---------------------- ���� �� ���������� ---------------------- //
        {
            if(!getHit)
            {
                if(WalkPositionCompleted())
                {
                    ChooseNewWalkPosition();
                    agent.SetDestination(_choosenWalkPosition);
                }
                if (IsSeePrey() && !player.isHidden) //���� ����� �������� � ���� ������ 
                {
                    StartPursuit(); //������� � ����� �������������
                }
            }
            
        }
    }
}