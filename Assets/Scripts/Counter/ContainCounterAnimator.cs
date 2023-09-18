using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] ContainCounter containCounter;
    private const string OPEN_CLOSE = "OpenClose";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containCounter.OnPlayerGrabbedObj += Player_OnPlayerGrabbedObj;
    }

    private void Player_OnPlayerGrabbedObj(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
