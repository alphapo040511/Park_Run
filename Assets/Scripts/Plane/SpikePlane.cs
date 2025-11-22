using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlane : PlaneBase
{
    public float damageAmount = 5f;
    private bool isStepped = false;

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

    // 타일 애니메이션 같은건 추후에 추가
}
