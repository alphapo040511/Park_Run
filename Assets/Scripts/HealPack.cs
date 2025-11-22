using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    public GameObject pivot;
    public float healAmount = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().TakeHeal(healAmount);
            pivot.gameObject.SetActive(false);
        }
    }
}
