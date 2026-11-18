using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplekeyinputjump : MonoBehaviour
{

    public Animator characterAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterAnimator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //this is for character animation when moving
        //we are checking if the key is down if so we
        //an animation we want
        if(Input.GetKeyDown(KeyCode.Space))
        {
            characterAnimator.SetTrigger("jumpTrig");
        }
        ///else if(Input.GetKeyDown(KeyCode.W))
        ///{
        /// characterAnimator.SetTrigger("walkTrig");
        //}
        else if(Input.GetKey(KeyCode.W))
        {
            characterAnimator.Play("Walk");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            characterAnimator.Play("walkleft");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            characterAnimator.Play("walkback");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            characterAnimator.Play("walkright");
        }
    }
}
