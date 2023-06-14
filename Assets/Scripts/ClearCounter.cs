using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private GameObject counterTopPoint;
    
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool test;
    private KitchenObject kitchenObject;

    private void Update() 
    {
        if (test && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
                
            }
        }
    }

    public void Interact()
    {
        
        if (kitchenObject == null)
        {
            GameObject kitchenGameObject = Instantiate(kitchenObjectSO.prefab, counterTopPoint.transform);
            kitchenGameObject.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
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
