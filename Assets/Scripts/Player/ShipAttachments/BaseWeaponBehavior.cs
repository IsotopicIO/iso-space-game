using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponBehavior : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    private bool InRange(GameObject enemy)
    {
        return true;
    }

    public void FireWeapon(GameObject enemy)
    {
        Debug.Log("here");
        if (InRange(enemy))
        {
            enemy.GetComponent<BaseEnemyShip>().Health -= this.damage;
            Debug.Log(enemy.GetComponent<BaseEnemyShip>().Health);
        }
    }
}
