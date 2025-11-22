using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoyingPoying : MonoBehaviour
{
    public PlayerController playerController;
    public float poyingSpeed = 5;

    public float jumpHeight = 0.5f;
    public float rotateAngle = 10f;

    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        if (!playerController.IsGrounded() || !PlayManager.instance.isPlaying) return;

        timer += Time.deltaTime * poyingSpeed;

        float amount = Mathf.PingPong(timer, 1);
        float half = Mathf.PingPong(timer/2, 1);

        float y = Mathf.Lerp(0, jumpHeight, amount);
        float rotate = Mathf.Lerp(-rotateAngle, rotateAngle, half);       // -rotateAngle ~ rotateAngle 범위

        transform.localPosition = Vector3.up * y;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotate));
    }
}
