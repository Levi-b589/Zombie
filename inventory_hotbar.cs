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

//the choices you have
public enum itemType { Gun1, Knife, pipeWrench, pills };