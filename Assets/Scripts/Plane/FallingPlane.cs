using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlane : PlaneBase
{
    public GameObject icon_1;
    public GameObject icon_2;
    public GameObject icon_3;

    public float fallingDuration = 3f;
    public float fallAcceleration = 9.81f;
    private bool isStepped = false;

    Coroutine fallingRoutine;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        IconSetting();
    }

    public override void OnTileEnter(PlayerController player)
    {
        if (isStepped) return;
        isStepped = true;

        if(fallingRoutine != null)
        {
            StopCoroutine(fallingRoutine);
        }

        fallingRoutine = StartCoroutine(Falling());
    }

    public override void OnTileInitialize()
    {
        isStepped = false;

        if (fallingRoutine != null)
        {
            StopCoroutine(fallingRoutine);
            fallingRoutine = null;
        }

        transform.localPosition = startPosition;
    }

    void IconSetting()
    {
        int round = PlaneManager.instance.GetRound();
        icon_1.SetActive(round == 0);
        icon_2.SetActive(round == 1);
        icon_3.SetActive(round == 2);
    }


    IEnumerator Falling()
    {
        Vector3 dir = -transform.up;
        float vel = 0;


        float t = 0;
        while(t < fallingDuration)
        {
            t += Time.deltaTime;
            vel += fallAcceleration * Time.deltaTime;

            transform.position += vel * dir * Time.deltaTime;

            yield return null;
        }

        fallingRoutine = null;
    }
}
