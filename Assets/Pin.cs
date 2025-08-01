using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Pin : MonoBehaviour
{
    public UnityEvent OnPulled;
    private Animator pinAnim;
    public bool blocked = false;

    void Start()
    {
        pinAnim = GetComponentInChildren<Animator>();
    }

    public void UnBlock() { blocked = false; }


    private void OnMouseDown()
    {
        if (blocked)
        {
            pinAnim.SetTrigger("Fail");
        }
        else
        {
            OnPulled?.Invoke();
            pinAnim.SetTrigger("Pull");
        }
    }

}
