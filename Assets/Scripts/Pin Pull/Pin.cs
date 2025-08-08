using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Pin : MonoBehaviour
{
    public int pinID;
    private Animator pinAnim;
    private BoxCollider2D clickBox;
    public bool blocked = false;

    void Start()
    {
        pinAnim = GetComponentInChildren<Animator>();
        clickBox = GetComponent<BoxCollider2D>();
    }

    
    public void UnBlock() { blocked = false; }


    private void OnMouseDown()
    {
        if (GameManager.Instance.gameOver) return;

        if (blocked)
        {
            pinAnim.SetTrigger("Fail");
        }
        else
        {
            pinAnim.SetTrigger("Pull");
            clickBox.enabled = false;
            GameManager.Instance.PinPulled(pinID);
        }
    }

}
