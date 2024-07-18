using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DroneStatus : MonoBehaviour
{
    public Slider batterySlider;
    public Slider waterTankSlider;
    public TextMeshProUGUI timerText;

    private float batteryLevel = 500.0f;
    private float waterLevel = 100.0f;
    private float timer = 300.0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (batteryLevel > 0)
        {
            batteryLevel -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && waterLevel > 0)
        {
            waterLevel -= Time.deltaTime * 10;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        batterySlider.value = batteryLevel;
        waterTankSlider.value = waterLevel;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

    }
}
