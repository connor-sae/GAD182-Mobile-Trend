using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        FindAnyObjectByType<WinLoseConditions>().ShowLosePanel();
    }
}
