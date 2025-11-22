using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaneType
{
    Default,
    Falling,
    Spike,
    None
}

public class PlaneModule : MonoBehaviour
{
    public PlaneBase defaultPlane;
    public PlaneBase spikePlane;
    public PlaneBase fallingPlane;

    public void SetPlane(PlaneType planeType)
    {
        if (defaultPlane == null || spikePlane == null || fallingPlane == null)
        {
            Debug.LogWarning("발판이 제대로 할당되지 않음");
            return;
        }

        InitializedPlane();

        switch (planeType)
        {
            case PlaneType.Default:
                defaultPlane.Show();
                break;
            case PlaneType.Falling:
                fallingPlane.Show();
                break;
            case PlaneType.Spike:
                spikePlane.Show();
                break;

            case PlaneType.None:
            default:
                // 아무런 발판도 키지 않는다.
                break;
        }
    }

    public void InitializedPlane()
    {
        if(defaultPlane == null)
            Debug.LogWarning("기본 발판 오브젝트가 등록 되지 않음");
        else
            defaultPlane.Hide();


        if (spikePlane == null)
            Debug.LogWarning("함정 오브젝트가 등록 되지 않음");
        else
            spikePlane.Hide();


        if (fallingPlane == null)
            Debug.LogWarning("떨어지는 발판 오브젝트가 등록 되지 않음");
        else
            fallingPlane.Hide();
    }
}
