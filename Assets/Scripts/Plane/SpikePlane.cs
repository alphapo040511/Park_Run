using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlane : PlaneBase
{
    public GameObject icon_1;
    public GameObject icon_2;
    public GameObject icon_3;

    public float damageAmount = 5f;
    private bool isStepped = false;

    private void OnEnable()
    {
        IconSetting();
    }

    public override void OnTileEnter(PlayerController player)
    {
        if (isStepped) return;
        isStepped = true;

        player.TakeDamage(damageAmount);
    }

    public override void OnTileInitialize()
    {
        isStepped = false;
    }



    void IconSetting()
    {
        int round = PlaneManager.instance.GetRound();
        icon_1.SetActive(round == 0);
        icon_2.SetActive(round == 1);
        icon_3.SetActive(round == 2);
    }
    // 타일 애니메이션 같은건 추후에 추가
}
