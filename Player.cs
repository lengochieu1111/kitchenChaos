using Pattern.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>, IKitchenObjectParent
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    [SerializeField] private bool _isWalking;
    [SerializeField] private float _moveSpeed = 7;
    [SerializeField] private LayerMask _countersLayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint;


    private float _playerRadius = 0.7f;
    private float _playerHeight = 2f;
    private float _moveDistance;

    private float _rotationSpeed = 10f;

    private float _interactDistance = 2f;
    private Vector3 _lastInteractDir;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._kitchenObjectHoldPoint == null )
        {
            this._kitchenObjectHoldPoint = this.transform.Find("KitchenObjectHoldPoint");
        }
    }

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
        if (this._selectedCounter != null)
        {
            this._selectedCounter.Interact(this);
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

        this._moveDistance = this._moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(this.transform.position, this.transform.position + Vector3.up * this._playerHeight, this._playerRadius, moveDir, this._moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(this.transform.position, this.transform.position + Vector3.up * this._playerHeight, this._playerRadius, moveDirX, this._moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(this.transform.position, this.transform.position + Vector3.up * this._playerHeight, this._playerRadius, moveDirZ, this._moveDistance);

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

        this.transform.forward = Vector3.Slerp(this.transform.forward, moveDir, Time.deltaTime * this._rotationSpeed);
    }
    
    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementInputNormalize();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            this._lastInteractDir = moveDir;
        }

        if (Physics.Raycast(this.transform.position, this._lastInteractDir, out RaycastHit raycastHit, this._interactDistance, this._countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (this._selectedCounter != baseCounter)
                {
                    this.SetSelectedCounter(baseCounter);
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

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this._selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = this._selectedCounter
        });

    }

    /*
     * IKitchenObjectParent
     */

    public Transform GetkitchenObjectFollowTransform()
    {
        return this._kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this._kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this._kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this._kitchenObject != null;
    }

}
