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
    public Attachment attachment;

    public void UseAttachment()
    {
        if (attachment != null)
        {
            attachment.Use();
        }
    }
}
