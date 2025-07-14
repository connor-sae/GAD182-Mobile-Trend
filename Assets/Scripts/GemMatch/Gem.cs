using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private int type;
    public float moveSpeed = 4;
    public float scaleSpeed = 6;
    public float mouseOverScaleFactor = 1.2f;
    public Vector2Int positionID;

    public int Type {
        get { return type; }
        set { type = value;
              GetComponent<SpriteRenderer>().sprite = GemMatchManager.Instance.gemSprites[value];
        }
    }

    public void GoTo(Vector2 pos)
    {
        targetPos = pos;
    }

    private void OnMouseDown()
    {
        Debug.Log("clickedr");
        FindAnyObjectByType<BoxController>().GemClicked(positionID.x, positionID.y);
    }


    private Vector2 targetPos;
    public bool selected = false;
    float defaultScale;

    private void Start()
    {
        GoTo(transform.position);
        defaultScale = transform.localScale.x;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        float currentScale = transform.localScale.x;
        if (selected)
            currentScale = Mathf.Lerp(currentScale, defaultScale * mouseOverScaleFactor, scaleSpeed * Time.deltaTime);
        else
            currentScale = Mathf.Lerp(currentScale, defaultScale, scaleSpeed * Time.deltaTime);

        transform.localScale = Vector3.one * currentScale;

    }

    
}
