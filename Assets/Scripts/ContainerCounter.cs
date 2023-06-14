using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        GameObject kitchenGameObject = Instantiate(kitchenObjectSO.prefab);
        kitchenGameObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }

}
