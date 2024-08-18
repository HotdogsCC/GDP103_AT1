using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnlarge : MonoBehaviour
{
    //target sizes when not hovered
    [SerializeField] private float normalX = 393.5f;
    [SerializeField] private float normalY = 125.1f;
    private RectTransform _transform;
    private bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        // lerps towards target dimensions, bigger when hovered and smaller when not
        if (pressed)
        {
            _transform.sizeDelta = Vector2.Lerp(_transform.sizeDelta, new Vector2(normalX + 50, normalY + 20), 5 * Time.deltaTime);
        }
        else
        {
            _transform.sizeDelta = Vector2.Lerp(_transform.sizeDelta, new Vector2(normalX, normalY), 5 * Time.deltaTime);
        }
    }

    //public functions called by the event manager on the buttons
    public void Enlarge()
    {
        pressed = true;
    }

    public void Shrink()
    {
        pressed = false;
    }
}
