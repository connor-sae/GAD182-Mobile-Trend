using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObsidianMorphing : MonoBehaviour
{
    public int[] ConversionLayers;
    public int ObsidianLayerIndex;
    public float forceNearConvertTime = 3;
    public float nearConvertRadius = 0.2f;
    bool converted = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (converted)
            return;

        if(ConversionLayers.Contains(collision.gameObject.layer))
        {
            Convert(collision.gameObject);
        }
    }

    public void Convert(GameObject otherObj)
    {
        otherObj.layer = ObsidianLayerIndex;
        otherObj.GetComponent<Rigidbody2D>().isKinematic = true;
        otherObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(otherObj.GetComponent<ObsidianMorphing>().ConvertRoutine());

        gameObject.layer = ObsidianLayerIndex;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        converted = true;
        //Invoke("ConvertNear", forceNearConvertTime);
    }

    public IEnumerator ConvertRoutine()
    {
        yield return new WaitForSeconds(forceNearConvertTime);
        ConvertNear();
    }


    private void ConvertNear()
    {
        LayerMask waterLavaGoldLayer = (1 << 6) | (1 << 7) | (1<<9); // 192
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, nearConvertRadius, waterLavaGoldLayer);

        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == 9) 
                Destroy(collider.gameObject);
            else
                Convert(collider.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, nearConvertRadius);
    }

    

}
