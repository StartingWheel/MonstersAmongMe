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
    [SerializeField] private Text keysText;
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
        _beforeHidePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.Rotate(0, 180, 0);
        transform.position = new Vector3(hidePosition.x,transform.position.y,hidePosition.z);
    }

    public void UnHide()
    {
        isHidden = false;
        _charController.Move(new Vector3(-(transform.position.x - _beforeHidePosition.x), 
            -(transform.position.y - _beforeHidePosition.y), 
            -(transform.position.z - _beforeHidePosition.z)));
    }

    public void FindHint()
    {
        _lastSave = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ++countHints;

    }

    public void Died()
    {
        Debug.Log("GAME OVER");
    }

    public void GetKey()
    {
        ++keys;
        keysText.text = keys.ToString();
    }

    public void BeCatched()
    {
        Debug.Log("Catched");
        _charController.Move(new Vector3(-(transform.position.x - _lastSave.x),
            -(transform.position.y - _lastSave.y),
            -(transform.position.z - _lastSave.z)));
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
        if (isHidden)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnHide();
            }
        }
    }
}
