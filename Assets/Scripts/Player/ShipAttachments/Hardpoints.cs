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
    public Types Type;
    public Size Size;
    public Attachment Attached;

    public void UseAttachment()
    {
        if (Attached != null)
        {
            Attached.Use();
        }
    }
}
