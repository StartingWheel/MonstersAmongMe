using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Material normalPlayer; //персонаж в нормальном состоянии
    [SerializeField] private Material imbaPlayer;   //персонаж после обращения 

    [SerializeField] private float normalSpeed = 6f; // нормальная скорость персонажа
    [SerializeField] private float imbaSpeed = 10f;  // скорость персонажа после обращения

    [SerializeField] private float imbaTime = 5f;   // время обращения в секундах
    [SerializeField] private int _hintsCount;
    [SerializeField] private Text keysText;
    [SerializeField] private SceneController _sc;

    private CharacterController _charController;
    public int countHints;

    public bool isImba; //состояние игрока. false - в нормальном состоянии, true - после обращения
    public bool isHidden; //спрятан ли игрок

    public int keys;

    private Vector3 _beforeHidePosition;

    private Coroutine _imbaCoroutine;
    private Vector3 _lastSave;

    // ================= Возвращение игрока в нормальное состояние ================== //
    public void ReturnToNormal()
    {
        GetComponent<Renderer>().sharedMaterial = normalPlayer;
        PlayerMovement.speed = normalSpeed;

        isImba = false;
        isHidden = false;
    }
    //================================================================================//

    // ============================== Обращение игрока ============================== //
    public void BecomeImba()
    {
        GetComponent<Renderer>().sharedMaterial = imbaPlayer;
        PlayerMovement.speed = imbaSpeed;
        isImba = true;

        _imbaCoroutine = StartCoroutine(OnImba());

    }
    //================================================================================//

    // ======================== Состояние игрока в обращении ======================== //
    private IEnumerator OnImba()
    {
        yield return new WaitForSecondsRealtime(imbaTime);
        ReturnToNormal();
    }
    //================================================================================//

    public IEnumerator ExitImba()
    {
        yield return new WaitForSecondsRealtime(2);
        StopCoroutine(_imbaCoroutine);
        ReturnToNormal();
    }

    public void Hide(Vector3 hidePosition)
    {
        isHidden = true;
        _beforeHidePosition = new Vector3(transform.position.x-1, transform.position.y, transform.position.z-1);
        transform.Rotate(0, 180, 0);
        transform.position = new Vector3(hidePosition.x,transform.position.y,hidePosition.z);
    }

    public void UnHide()
    {
        isHidden = false;
        GetComponent<CharacterController>().enabled = false;
        transform.position = _beforeHidePosition;
        GetComponent<CharacterController>().enabled = true;
    }

    public void FindHint()
    {
        Debug.Log("Find Hint");
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
