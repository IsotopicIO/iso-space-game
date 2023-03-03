using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponBehavior : Attachment, I_Activity
{
    //properties
    protected bool IsOnCooldown = false;

    protected float Cooldown = 0f;
    protected float ShotTime = 0f;

    public float Damage = 10f;
    public float Range;
    public float MaxHitAngle;

    public int ShotsPerMinute = 0;

    public Status Status => CanFire();

    //methods
    public bool CanFire()
    {
        return Time.time > ShotTime + Cooldown;
    }

    protected bool InRange(Transform enemy)
    {
        var shipTransform = GameManagement.Instance.ShipController.transform;

        if (Vector3.Angle(enemy.position - shipTransform.position, transform.forward) > MaxHitAngle) return false;

        if ((shipTransform.position - enemy.position).magnitude > Range) return false;

        return true;
    }
}
