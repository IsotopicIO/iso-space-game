using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponBehavior : MonoBehaviour
{
    
    protected float ShotTime = 0;

    public float Damage = 10f;


    public abstract bool CanFire();

    public abstract void FireWeapon();
}
