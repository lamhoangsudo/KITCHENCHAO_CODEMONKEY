using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAminator : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    [SerializeField] CuttingCounter cuttingCounter;
    private const string CUT = "Cut";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnPrecessAminator += CuttingCounter_OnPrecessAminator;
    }

    private void CuttingCounter_OnPrecessAminator(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
