using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Usable
{
    public void Use();
}

public interface I_Activity
{
    public Status Status { get; }
}