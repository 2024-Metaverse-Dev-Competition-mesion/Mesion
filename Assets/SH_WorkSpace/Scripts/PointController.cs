using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public GameObject[] points;
    private int currentPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(false);
        }

        if (points.Length > 0)
        {
            points[0].SetActive(true);
        }
    }

    public void OnPointReached()
    {
        points[currentPointIndex].SetActive(false);

        currentPointIndex++;

        if (currentPointIndex < points.Length)
        {
            points[currentPointIndex].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
