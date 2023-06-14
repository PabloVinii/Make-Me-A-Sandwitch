using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private GameObject counterTopPoint;

    public void Interact()
    {
        Debug.Log("interact");
        GameObject kitchenObject = Instantiate(kitchenObjectSO.prefab, counterTopPoint.transform);
        kitchenObject.transform.localPosition = Vector3.zero;

        Debug.Log(kitchenObject.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
