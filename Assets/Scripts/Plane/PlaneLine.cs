using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLine : MonoBehaviour
{
    public PlaneModule planeReference;
    private List<PlaneModule> planes = new List<PlaneModule>();
    private int index = 0;

    public GameObject RotateObstacle;
    public GameObject YoyoObstacle;
    public GameObject healPack;

    public void InitializeLine(int planeCount, int radius)
    {
        float scale = GetWidth(planeCount, radius);
        for(int i = 0; i < planeCount; i++)
        {
            PlaneModule plane = Instantiate(planeReference, transform);

            float theta = 2f * Mathf.PI * i / planeCount;                    // 라디안
            Vector3 pos = new Vector3(Mathf.Cos(theta),Mathf.Sin(theta), 0f) * radius;

            plane.transform.localPosition = pos;

            Vector3 dir = transform.position - plane.transform.position;
            plane.transform.up = dir.normalized;

            plane.transform.localScale = scale * Vector3.one;

            planes.Add(plane);
        }
    }

    public void ActiveObstacle()
    {
        if (Random.value < 0.5f)
            RotateObstacle.SetActive(true);
        else
            YoyoObstacle.SetActive(true);
    }

    public void ResetLine()
    {
        foreach(var plane in planes)
        {
            plane.InitializedPlane();
        }
    }

    public void RandomPlane()
    {
        int count = 0;
        foreach (var plane in planes)
        {
            float x = index * 0.2f;                 // 길이 방향 스케일
            float y = 2 * (float)count++ / planes.Count; // 둘레 방향 0~1, 자동으로 이어짐

            float value = Mathf.PerlinNoise(x, y);

            if (value < 0.5f)
                plane.SetPlane(PlaneType.Default);
            else if (value < 0.7f)
                plane.SetPlane(PlaneType.None);
            else if (value < 0.85f)
                plane.SetPlane(PlaneType.Falling);
            else
                plane.SetPlane(PlaneType.Spike);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        RotateObstacle.SetActive(false);
        YoyoObstacle.SetActive(false);
        healPack.SetActive(false);
    }

    public void Show(Vector3 position, int index)
    {
        transform.position = position;
        this.index = index;
        gameObject.SetActive(true);
    }

    public static float GetWidth(int planeCount, int radius)
    {
        float theta = Mathf.PI / planeCount;
        return 2 * radius * Mathf.Tan(theta);
    }
}
