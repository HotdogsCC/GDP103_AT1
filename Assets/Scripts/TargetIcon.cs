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

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position + displayOffset);
        Debug.Log(iconObject.transform.position);
        if (iconObject.transform.position.z < 0)
        {
            iconObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            iconObject.GetComponent<Image>().color = Color.white;
        }
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
