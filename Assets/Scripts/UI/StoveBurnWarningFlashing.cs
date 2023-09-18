using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningFlashing : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private float timeToShowWarning;
    private Animator animator;
    private const string ON_WARNING_FLASHING = "OnFlashing";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }private void Start()
    {
        animator.SetBool(ON_WARNING_FLASHING, false);
        stoveCounter.OnProcessBar += StoveCounter_OnProcessBar;
    }

    private void StoveCounter_OnProcessBar(object sender, IHasProcess.OnPrecessChangerEventArgs e)
    {
        if (stoveCounter.IsFried() && e.processNormalized > timeToShowWarning)
        {
            animator.SetBool(ON_WARNING_FLASHING, true);
        }
        else
        {
            animator.SetBool(ON_WARNING_FLASHING, false);
        }
    }
}
