using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponBehavior : MonoBehaviour
{
    private bool IsOnCooldown;
    
    private float Cooldown;
    private float ShotTime = 0;

    public float Damage = 10f;
    public float Range = 100f;

    public int DamageRadius;
    public int ShotsPerMinute;

    private void Awake()
    {
        this.Cooldown = ShotsPerMinute / 60;
        Debug.Log(this.Cooldown + " seconds between each shot");
    }

    private bool CanFire()
    {
        return Time.time > ShotTime + Cooldown;
    }

    private bool InRange(GameObject enemy)
    {
        var EnemyPosition = enemy.transform.position;
        var PlayerPosition = this.transform.parent.position;
        if (PlayerPosition.z + this.Range <= EnemyPosition.z && PlayerPosition.x - this.DamageRadius <= EnemyPosition.x && PlayerPosition.x + this.DamageRadius >= EnemyPosition.x && PlayerPosition.y - this.DamageRadius <= EnemyPosition.y && PlayerPosition.y + this.DamageRadius >= EnemyPosition.y)
        {
            return true;
        }

        return false;
    }

    public void FireWeapon(GameObject enemy)
    {
        if (CanFire())
        {
            ShotTime = Time.time;
            if (InRange(enemy))
            {
                enemy.GetComponent<BaseEnemyShip>().Health -= this.Damage;
                Debug.Log(enemy.GetComponent<BaseEnemyShip>().Health);
            }
            else
            {
                Debug.Log("Miss");
            }
        }
    }
}
