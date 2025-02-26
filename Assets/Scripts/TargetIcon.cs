using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIcon : MonoBehaviour
{
    public GameObject iconPrefab;
    private GameObject iconObject;
    public Vector3 displayOffset = new Vector3(0, 0.6f, 0);
    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update

    private void Start()
    {
        if (canvas == null)
        {
            Debug.Log("lol no canvas");
            return;
        }

        iconObject = Instantiate(iconPrefab, canvas.transform);
    }

    // Update is called once per frame
    private void Update()
    {
        if(iconObject == null)
        {
            return;
        }

        //sets position of icon to where ever it is in screen space
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position + displayOffset);

        //fixes issue where the icon mirrors when facing opposite direction
        if (screenPosition.z < 0)
        {
            iconObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            iconObject.GetComponent<Image>().color = Color.white;
            
        }

        //clamps position in case it is off screen
        screenPosition = new Vector3(Mathf.Clamp(screenPosition.x, 10, Screen.width - 10), Mathf.Clamp(screenPosition.y, 10, Screen.height - 10), 0);

        iconObject.transform.position = screenPosition;
        
    }
    private void OnDisable()
    {
        if(iconObject != null)
        {
            Destroy(iconObject);
        }
    }
}
