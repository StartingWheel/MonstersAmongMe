using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Hint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _player; //�����
    [SerializeField] private Player _playerPl;
    [SerializeField] private float _distance;    //���������� ���������� ����� ������� � ������ ��� ������, ����� ���������� 
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _textTime;
    [SerializeField] private SceneController _sc;
    [SerializeField] private Image _hintImage;
    [SerializeField] private Sprite _hintTexture;
    public void OnPointerClick(PointerEventData eventData)
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            Vector3 heading = transform.position - _player.transform.position;
            if (heading.sqrMagnitude <= _distance * _distance)
            {
                _text.enabled = false;
                _playerPl.FindHint();
                _hintImage.sprite = _hintTexture;
                _sc.HintOn();
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