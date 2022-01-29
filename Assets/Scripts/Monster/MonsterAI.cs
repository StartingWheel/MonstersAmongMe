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
    [SerializeField] private List<Vector3> _walkPositions = new List<Vector3>(); //����� ����������� ������� 
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioSource _scream;

    private Animator myAnimator; //�������� �������

    private Vector3 _choosenWalkPosition; //��������� �����������
    
    public bool isPursuitMode; //������� �� ����� �������������

    public bool isCatch;  //������ �� ������ 
    public bool getHit; // ������� ����

    public void OnDestroy()
    {
        _sound.Stop();
        _scream.Stop();
    }

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
        if (Physics.SphereCast(new Ray(transform.position, transform.right*(-1)), 0.20f, out hit))
        {
            if (hit.transform.gameObject.GetComponent<Player>())
            {
                return true;
            }
        }
        if (Physics.SphereCast(new Ray(transform.position, transform.right), 0.20f, out hit))
        {
            if (hit.transform.gameObject.GetComponent<Player>())
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
        myAnimator.Play("Run");
        _scream.Play();
    }
    //==============================================================================================//

    // ================================ ����������� � ������� ����� =============================== //
    private void EndPursuit()
    {
        isPursuitMode = false;
        transform.Rotate(0, 180, 0);
        if (!getHit) myAnimator.Play("Walk");
        agent.speed = normalSpeed; //������� �� ���
        ChooseNewWalkPosition();
        agent.SetDestination(_choosenWalkPosition);
        _scream.Stop();
    }
    //==============================================================================================//


    private IEnumerator OnStun()
    {
        myAnimator.Play("Stun");
        _sound.Stop();
        agent.enabled = false;
        yield return new WaitForSeconds(_stunTime);
        getHit = false;
        agent.enabled = true;
        myAnimator.Play("Walk");
        ChooseNewWalkPosition();
        agent.SetDestination(_choosenWalkPosition);
        _sound.Play();
    }

    void Update()
    {
        if (!getHit)
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

                if (player.isHidden) //���� ����� ���������
                {
                    EndPursuit();
                }

            }
            else
            // ---------------------- ���� �� ���������� ---------------------- //
            {
                if (WalkPositionCompleted())
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
