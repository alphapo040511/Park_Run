using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaneBase: MonoBehaviour
{
    public abstract void OnTileEnter(PlayerController player);
    public abstract void OnTileInitialize();

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        OnTileInitialize();
        gameObject.SetActive(false);
    }
}
