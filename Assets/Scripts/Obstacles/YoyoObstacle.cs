using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoObstacle : MonoBehaviour
{
    public float outLine = -6f;

    public float range = 2f;

    private float t = 0;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        float pos = Mathf.PingPong(t, range);

        transform.localPosition = transform.up * (outLine + pos);
    }
}
