using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Material normalPlayer; //�������� � ���������� ���������
    [SerializeField] private Material imbaPlayer;   //�������� ����� ��������� 

    [SerializeField] private float normalSpeed = 6f; // ���������� �������� ���������
    [SerializeField] private float imbaSpeed = 10f;  // �������� ��������� ����� ���������

    [SerializeField] private float imbaTime = 5f;   // ����� ��������� � ��������
    [SerializeField] private int _hintsCount;
    [SerializeField] private Text keysText;
    [SerializeField] private SceneController _sc;
    [SerializeField] private AudioSource _hiddenAudio;
    [SerializeField] private AudioSource _audio;

    private CharacterController _charController;
    private int countHints;

    public bool isImba; //��������� ������. false - � ���������� ���������, true - ����� ���������
    public bool isHidden; //������� �� �����

    public int keys;

    private Vector3 _beforeHidePosition;

    private Coroutine _imbaCoroutine;
    private Vector3 _lastSave;

    // ================= ����������� ������ � ���������� ��������� ================== //
    public void ReturnToNormal()
    {
        GetComponent<Renderer>().sharedMaterial = normalPlayer;
        PlayerMovement.speed = normalSpeed;

        isImba = false;
        isHidden = false;
    }
    //================================================================================//

    // ============================== ��������� ������ ============================== //
    public void BecomeImba()
    {
        GetComponent<Renderer>().sharedMaterial = imbaPlayer;
        PlayerMovement.speed = imbaSpeed;
        isImba = true;

        _imbaCoroutine = StartCoroutine(OnImba());

    }
    //================================================================================//

    // ======================== ��������� ������ � ��������� ======================== //
    private IEnumerator OnImba()
    {
        yield return new WaitForSeconds(imbaTime);
        ReturnToNormal();
    }
    //================================================================================//

    public IEnumerator ExitImba()
    {
        yield return new WaitForSeconds(2);
        StopCoroutine(_imbaCoroutine);
        ReturnToNormal();
    }

    public void Hide(Vector3 hidePosition)
    {
        isHidden = true;
        _beforeHidePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.Rotate(0, 180, 0);
        transform.position = new Vector3(hidePosition.x,transform.position.y,hidePosition.z);
        _hiddenAudio.Play();
    }

    public void UnHide()
    {
        isHidden = false;
        GetComponent<CharacterController>().enabled = false;
        transform.position = _beforeHidePosition;
        GetComponent<CharacterController>().enabled = true;
        _hiddenAudio.Stop();
    }

    public void FindHint()
    {
        _audio.Play();
        _lastSave = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ++countHints;

    }

    public void Died()
    {
        if (countHints >= _hintsCount)
        {
            _sc.End2On();
        } else
        {
            _sc.LooseOn();
        }
    }

    public void GetKey()
    {
        ++keys;
        keysText.text = keys.ToString();
    }

    public void BeCatched()
    {
        Debug.Log("Catched");
        GetComponent<CharacterController>().enabled = false;
        transform.position = _lastSave;
        GetComponent<CharacterController>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ReturnToNormal();
        keys = 0;
        countHints = 0;
        _charController = GetComponent<CharacterController>();
        _lastSave = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isHidden = false;
            BeCatched();
        }
        if (isHidden)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnHide();
            }
        }
    }
}
