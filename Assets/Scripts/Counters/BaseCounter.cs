using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private GameObject counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        
    }

    public virtual void InteractAlternate(Player player)
    {
        
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint.transform;
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
