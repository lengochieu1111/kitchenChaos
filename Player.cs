using Pattern.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs :EventArgs
    {
        public ClearCounter selectedCounter;
    }


    [SerializeField] private bool _isWalking;
    [SerializeField] private float _moveSpeed = 7;
    [SerializeField] private LayerMask _countersLayerMask;

    private float playerRadius = 0.7f;
    private float playerHeight = 2f;
    private float moveDistance;

    float rotationSpeed = 10f;

    private float interactDistance = 2f;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    protected override void SetupValues()
    {
        base.SetupValues();

        this._countersLayerMask = LayerMask.GetMask(LayerMaskString.Counters);
    }

    protected override void Start()
    {
        base.Start();

        GameInput.Instance.OnInteractAction += Instance_OnInteractAction;
    }

    private void Instance_OnInteractAction(object sender, System.EventArgs e)
    {
        if (this.selectedCounter != null)

        {
            this.selectedCounter.Interact();
        }
    }

    private void Update()
    {
        this.HandleMovement();
        this.HandleInteractions();

    }


    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementInputNormalize();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        this.moveDistance = this._moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(this.transform.position, this.transform.position + Vector3.up * this.playerHeight, this.playerRadius, moveDir, this.moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(this.transform.position, this.transform.position + Vector3.up * this.playerHeight, this.playerRadius, moveDirX, this.moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(this.transform.position, this.transform.position + Vector3.up * this.playerHeight, this.playerRadius, moveDirZ, this.moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            this.transform.position += moveDir * this._moveSpeed * Time.deltaTime;
        }

        this._isWalking = moveDir != Vector3.zero;

        this.transform.forward = Vector3.Slerp(this.transform.forward, moveDir, Time.deltaTime * this.rotationSpeed);
    }
    
    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementInputNormalize();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            this.lastInteractDir = moveDir;
        }

        if (Physics.Raycast(this.transform.position, this.lastInteractDir, out RaycastHit raycastHit, this.interactDistance, this._countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (this.selectedCounter != clearCounter)
                {
                    this.SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                this.SetSelectedCounter(null);
            }
        }
        else
        {
            this.SetSelectedCounter(null);
        }

    }


    // Setter Getter 

    public bool IsWalking()
    {
        return this._isWalking;
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = this.selectedCounter
        });

    }

}
