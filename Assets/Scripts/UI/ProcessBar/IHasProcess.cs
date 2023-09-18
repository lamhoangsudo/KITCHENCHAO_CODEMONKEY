using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProcess
{
    public event EventHandler<IHasProcess.OnPrecessChangerEventArgs> OnProcessBar;
    public class OnPrecessChangerEventArgs : EventArgs
    {
        public float processNormalized;
    }
}
