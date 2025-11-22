using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLine : MonoBehaviour
{
    public PlaneModule planeReference;
    private List<PlaneModule> planes = new List<PlaneModule>();

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

    public void ResetLine()
    {
        foreach(var plane in planes)
        {
            plane.InitializedPlane();
        }
    }

    public void RandomPlane()
    {
        foreach (var plane in planes)
        {
            //int ran = Random.Range(0, 4);
            //plane.SetPlane((PlaneType)ran);

            float value = Random.value;
            if(value < 0.65f)
            {
                plane.SetPlane(PlaneType.Default);
            }
            else if(value < 0.8f)
            {
                plane.SetPlane(PlaneType.Falling);
            }
            else if(value < 0.9f)
            {
                plane.SetPlane(PlaneType.Spike);
            }
            else
            {
                plane.SetPlane(PlaneType.None);
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public static float GetWidth(int planeCount, int radius)
    {
        float theta = Mathf.PI / planeCount;
        return 2 * radius * Mathf.Tan(theta);
    }
}
