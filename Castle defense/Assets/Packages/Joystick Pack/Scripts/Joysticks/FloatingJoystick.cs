using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.anchoredPosition = Vector2.zero;
        ResetInput();
    }

    private void OnDisable()
    {
        ResetInput();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    private void ResetInput()
    {
        OnPointerDown(new PointerEventData(EventSystem.current));
        background.gameObject.SetActive(false);
    }
}