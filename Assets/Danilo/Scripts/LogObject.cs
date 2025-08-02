using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LogObject : MonoBehaviour
{
    [Tooltip ("If Left unassigned will snap to position")]
    [SerializeField] private Animator logAnimator;

    [HideInInspector] public string text;
    private TMP_Text tmp_text;

    private void Start()
    {
        tmp_text = GetComponent<TMP_Text>();
        if(text == "")
        {
            tmp_text.color = Color.red;
            tmp_text.text = "NO TEXT ASSIGNED";
        }else
        {
            tmp_text.text = text;
        }
    }

    public void Push()
    {
        if(logAnimator == null)
        {
            transform.position += new Vector3(0, (transform as RectTransform).rect.height, 0);
        }else
        {
            logAnimator.SetTrigger("Push");
        }
    }

    public void Remove()
    {
        if (logAnimator == null)
        {
            Destroy(gameObject);
        }
        else
        {
            logAnimator.SetTrigger("Remove");
        }
    }
}
