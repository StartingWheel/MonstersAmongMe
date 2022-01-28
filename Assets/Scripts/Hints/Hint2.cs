using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Hint2 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _player; //игрок
    [SerializeField] private float _distance;    //допустимое расстояние между игроком и местом для пряток, чтобы спрятаться 
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _textTime;
    [SerializeField] private string _hintText;
    [SerializeField] private SceneController _sc;
    [SerializeField] private TextMeshProUGUI _UIHinttext;

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        Vector3 heading = transform.position - _player.transform.position;
        if (heading.sqrMagnitude <= _distance * _distance)
        {
            _text.enabled = false;
            _UIHinttext.text = _hintText;
            _player.GetComponent<Player>().FindHint();
            _sc.Hint2On();
            Destroy(this.gameObject);
        }
        else
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
}
