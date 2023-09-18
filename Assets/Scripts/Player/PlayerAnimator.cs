using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALIKING = "IsWalking";
    [SerializeField]
    private PlayerController playerController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool(IS_WALIKING, playerController.IsWalking());
    }
}
