using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalWeapon : BaseWeaponBehavior
{
    private void Awake()
    {
        this.Cooldown = ShotsPerMinute / 60;
        Debug.Log(this.Cooldown + " seconds between each shot");
    }

    public override void Use()
    {
        if (CanFire())
        {
            ShotTime = Time.time;
            foreach (var enemy in GameManagement.ActiveEnemies)
            {
                if (InRange(enemy.transform))
                {
                    enemy.GetComponent<BaseEnemyShip>().Health -= Damage;
                    Debug.Log(enemy.GetComponent<BaseEnemyShip>().Health);
                }
                else
                {
                    Debug.Log("Miss");
                }
            }
        }
    }
}
