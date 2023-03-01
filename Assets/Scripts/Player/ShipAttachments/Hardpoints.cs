using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Types
{
    AllPurpose,
    Offensive,
    Defensive
}

public enum Size
{
    Large,
    Medium,
    Small
}

public class Hardpoints : MonoBehaviour
{
    public Types type;
    public Size size;
    public BaseWeaponBehavior weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
