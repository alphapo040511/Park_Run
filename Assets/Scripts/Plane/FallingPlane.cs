using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlane : PlaneBase
{
    public float fallingDuration = 3f;
    public float fallSpeed = 9.8f;
    private bool isStepped = false;

    Coroutine fallingRoutine;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
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


    IEnumerator Falling()
    {
        Vector3 dir = -transform.up;

        float t = 0;
        while(t < fallingDuration)
        {
            t += Time.deltaTime;

            transform.position += fallSpeed * dir * Time.deltaTime; ;

            yield return null;
        }

        fallingRoutine = null;
    }
}
