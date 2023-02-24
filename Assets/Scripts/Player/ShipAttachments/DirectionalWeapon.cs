using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalWeapon : BaseWeaponBehavior
{
    public float Range = 120f;
    public float MaxHitAngle;

    public int ShotsPerMinute;

    protected bool IsOnCooldown;

    protected float Cooldown;

    private void Awake()
    {
        this.Cooldown = ShotsPerMinute / 60;
        Debug.Log(this.Cooldown + " seconds between each shot");
    }

    public override bool CanFire()
    {
        return Time.time > ShotTime + Cooldown;
    }

    private bool InRange(Transform enemy)
    {
        var shipTransform = GameManagement.Instance.ShipController.transform;

        if (Vector3.Angle(enemy.position - shipTransform.position, transform.forward) > MaxHitAngle) return false;

        if ((shipTransform.position - enemy.position).magnitude > Range) return false;

        return true;
    }

    public override void FireWeapon()
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
