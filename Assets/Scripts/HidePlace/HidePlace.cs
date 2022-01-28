using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HidePlace : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _player; //игрок
    [SerializeField] private float _distance;//допустимое расстояние между игроком и местом для пряток, чтобы спрятаться 
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _textTime;
    public bool isUse;

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            Vector3 heading = transform.position - _player.transform.position;
            if (heading.sqrMagnitude <= _distance*_distance)
            {
                isUse = true;
                _text.enabled = false;
                if (!_player.GetComponent<Player>().isHidden)
                    _player.GetComponent<Player>().Hide(transform.position);
            } else
            {
                _text.enabled = true;
                StartCoroutine(TextShow());
            }

        //}
    }

    private IEnumerator TextShow()
    {
        yield return new WaitForSeconds(_textTime);
        _text.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        _text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
