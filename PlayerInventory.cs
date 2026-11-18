//Levi Bunde
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//https://www.youtube.com/watch?v=HGol5qhqjOE
//link that got some of the code from 

public class PlayerInventory : MonoBehaviour
{
    [Header("General")]
    public List<itemType> inventoryList;
    public int selectedItem;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    [Space(20)]
    [Header("Item gameobjects")]
    //1. there are two parts that you will need to edit if you wanted to 
    //add more items for the game. this is part one 
    [SerializeField] GameObject gun1;
    [SerializeField] GameObject pipeWrench;
    [SerializeField] GameObject pills;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };

    private void Start()
    {
    //2 this is part two just copy what code there is and just change name.
        itemSetActive.Add(itemType.Gun1, gun1);
        itemSetActive.Add(itemType.pills, pills);
        itemSetActive.Add(itemType.pipeWrench, pipeWrench);

        NewItemSelected();
    }
    //this is for swapping to different items for the hot bar
    //can add more up to 9 because then you just run out of keys
    //
    void Update()
    {
    //keep this to 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 1)
        {
            selectedItem = 1;
            NewItemSelected();
        }
        // change key to 2 
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 2)
        {
            selectedItem = 2;
            NewItemSelected();
        }
        //change key to 3
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 3)
        {
            selectedItem = 3;
            NewItemSelected();
        }
    }

    private void NewItemSelected()
    {
        gun1.SetActive(false);
        pipeWrench.SetActive(false);
        pills.SetActive(false);

        GameObject selectedItemGameObject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameObject.SetActive(true);
    }
}
