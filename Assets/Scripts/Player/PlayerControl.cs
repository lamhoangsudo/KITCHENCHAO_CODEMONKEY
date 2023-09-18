using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

//Singleton Pattern: class Singleton
public class PlayerController : MonoBehaviour, IKitchenObjParent
{
    //Make sure the class has only one instance
    //Static: Guaranteed to be accessible anytime, anywhere
    public static PlayerController player { get; private set; }
    [SerializeField] private float height = 2f;
    [SerializeField] private float size = 1f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactDis = 1.1f;
    [SerializeField] private LayerMask interactLayerMask;
    public event EventHandler<OnSelectCounterChangeEventArg> OnSelectCounterChange;
    public event EventHandler OnPickSomeThing;
    private BaseCounter selectCounter;
    private Vector3 lastDirInteract;
    private bool isWalking = false;
    private KitchenObj kitchenObj;
    [SerializeField] private Transform holdPoint;

    public class OnSelectCounterChangeEventArg : EventArgs
    {
        public BaseCounter selectCounter;
    }
    //Just-in-time initialization or initialization on the first call
    private void Awake()
    {
        player = this;
    }
    // Update is called once per frame
    private void Start()
    {
        //listen event
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectCounter != null && GameHandler.gameHandler.OnPLay())
        {
            selectCounter.InteractAlternate(this);
        }
    }

    //process action
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectCounter != null && GameHandler.gameHandler.OnPLay())
        {
            selectCounter.Interact(this);
        }
    }

    private void Update()
    {
        HanderMovement();
        HanderOfInteraction();
    }
    private void HanderOfInteraction() 
    {
        Vector3 dir = new(gameInput.GetVectorMovementnormalized().x, 0f, gameInput.GetVectorMovementnormalized().y);
        if (dir != Vector3.zero)
        {
            lastDirInteract = dir;
        }
        if (Physics.Raycast(transform.position, lastDirInteract, out RaycastHit raycastHit, interactDis, interactLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void HanderMovement()
    {
        if (gameInput.GetVectorMovementnormalized() != Vector2.zero)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        //isWalking = inputVector != Vector3.zero;
        Vector3 dir = new(gameInput.GetVectorMovementnormalized().x, 0f, gameInput.GetVectorMovementnormalized().y);
        float dis = moveSpeed * Time.deltaTime;
        bool canMove = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, size, dir, dis);
        if (canMove)
        {
            Vector3 moveDirX = new Vector3(dir.x, 0, 0).normalized;
            canMove = moveDirX.x != 0 &&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, size, moveDirX, dis);
            if (canMove)
            {
                dir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, dir.z).normalized;
                canMove = moveDirZ.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, size, moveDirZ, dis);
                if (canMove)
                {
                    dir = moveDirZ;
                }
            }
        }
        else
        {
            transform.position += dis * dir.normalized;
        }
        transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * rotationSpeed);
    }
    private void SetSelectedCounter(BaseCounter selectCounter)
    {
        if (!(this.selectCounter == selectCounter && selectCounter != null))
        {
            this.selectCounter = selectCounter;
        }
        OnSelectCounterChange?.Invoke(this, new OnSelectCounterChangeEventArg { 
            selectCounter = selectCounter
        });
    }
    public bool IsWalking() {
        return isWalking;
    }

    public Transform GetCounterTop()
    {
        return holdPoint;
    }
    public KitchenObj GetKitchenObj()
    {
        return kitchenObj;
    }
    public void SetKitchenObj(KitchenObj kitchenObj)
    {
        this.kitchenObj = kitchenObj;
        if (kitchenObj != null)
        {
            OnPickSomeThing?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ClearKitchenObj()
    {
        kitchenObj = null;
    }
    public bool HasKitchenObj()
    {
        return kitchenObj != null;
    }
}
