using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline _outline;
    [SerializeField] private int outlineWidth = 10;//толщина обводки при наведении 
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _outline.OutlineWidth = outlineWidth;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _outline.OutlineWidth = 0;
    }
}