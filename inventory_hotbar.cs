using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Object/Item")]
public class inventory_hotbar : ScriptableObject
{
    [Header("Properties")]
    public itemType item_type;
    public Sprite item_Sprite;
}

public enum itemType { Gun1,pipeWrench,pills};