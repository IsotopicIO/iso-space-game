using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attachment : MonoBehaviour
{
    public Types type = Types.AllPurpose;

    //placement for future ticket
    public void WillFit()
    {
    }

    public abstract void Use();
}
