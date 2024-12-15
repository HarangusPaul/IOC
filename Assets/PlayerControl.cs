using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public float movSpeed;

    private float speadX, speedY;
    private Rigidbody2D rb;

    public Button up;
    public Button down;
    public Button right;
    public Button left;

    private bool isUpPressed, isDownPressed, isRightPressed, isLeftPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (up != null)
        {
            AddButtonListeners(up, OnUpPressed, OnUpReleased);
            up.GetComponentInChildren<TextMeshProUGUI>().text = "up";
        }

        if (down != null)
        {
            AddButtonListeners(down, OnDownPressed, OnDownReleased);
            down.GetComponentInChildren<TextMeshProUGUI>().text = "down";
        }

        if (right != null)
        {
            AddButtonListeners(right, OnRightPressed, OnRightReleased);
            right.GetComponentInChildren<TextMeshProUGUI>().text = "right";
        }

        if (left != null)
        {
            AddButtonListeners(left, OnLeftPressed, OnLeftReleased);
            left.GetComponentInChildren<TextMeshProUGUI>().text = "left";
        }
    }

    void Update()
    {
        // Handle keyboard input
        float horizontal = Input.GetAxisRaw("Horizontal") * movSpeed;
        float vertical = Input.GetAxisRaw("Vertical") * movSpeed;

        // Override keyboard input with button input if a button is pressed
        if (isLeftPressed) horizontal = -movSpeed;
        if (isRightPressed) horizontal = movSpeed;
        if (isUpPressed) vertical = movSpeed;
        if (isDownPressed) vertical = -movSpeed;

        // Update Rigidbody velocity
        rb.linearVelocity = new Vector2(horizontal, vertical);
    }

    private void AddButtonListeners(Button button, UnityEngine.Events.UnityAction onPressed, UnityEngine.Events.UnityAction onReleased)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDown.callback.AddListener((data) => { onPressed(); });
        trigger.triggers.Add(pointerDown);

        EventTrigger.Entry pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUp.callback.AddListener((data) => { onReleased(); });
        trigger.triggers.Add(pointerUp);
    }

    // Button press handlers
    private void OnUpPressed() { isUpPressed = true; }
    private void OnUpReleased() { isUpPressed = false; }

    private void OnDownPressed() { isDownPressed = true; }
    private void OnDownReleased() { isDownPressed = false; }

    private void OnLeftPressed() { isLeftPressed = true; }
    private void OnLeftReleased() { isLeftPressed = false; }

    private void OnRightPressed() { isRightPressed = true; }
    private void OnRightReleased() { isRightPressed = false; }
}
