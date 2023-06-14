using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance {get; private set;}

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayer;
    private GameInput gameInput;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    [SerializeField] private GameObject kitchenObjectHoldPoint;

    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player Instance");
        }
        Instance = this;
        gameInput = GameObject.FindWithTag("GameInput").GetComponent<GameInput>();
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteraction;
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit rayHit, interactDistance, countersLayer))
        {
            if (rayHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //has clearCounter
                if (baseCounter != selectedCounter)
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

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerheight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;    
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //can move only on the X
                moveDir = moveDirX;
            } 
            else
            {
                // cannot move only on the X

                //attempt only Z movement    
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;  
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 12f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint.transform;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearkitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
