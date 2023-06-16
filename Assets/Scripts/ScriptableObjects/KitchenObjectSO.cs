using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchen Object", menuName = "Kitchen/New Kitchen Item")]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
}
