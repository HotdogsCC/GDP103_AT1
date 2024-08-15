using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    ThirdPersonController player;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
        slider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player._speed;
    }
}
