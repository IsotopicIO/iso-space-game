using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Status
{
    public virtual bool Active { get; protected set; }

    public virtual void Set(bool value)
    {
        Active = value;
    }

    public static implicit operator Status(bool value)
    {
        return new Status(){ Active = value };
    }
}

public class NotifyingStatus : Status
{
    public delegate void StatusChangedHandler(object source, StatusChangedEventArgs args);
    public event StatusChangedHandler OnStatusChanged;
    public class StatusChangedEventArgs : EventArgs
    {
        public bool NewValue;
    }

    public override bool Active 
    { 
        get => base.Active; 
        protected set {
            bool prev = base.Active;
            base.Active = value;
            if (value != prev)
            {
                OnStatusChanged?.Invoke(this, new StatusChangedEventArgs() { NewValue = value });
            }
        } 
    }
}