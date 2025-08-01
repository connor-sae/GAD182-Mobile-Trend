using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObsidianMorphing : MonoBehaviour
{
    public int[] ConversionLayers;
    public int ObsidianLayerIndex;
    bool converted = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (converted)
            return;

        if(ConversionLayers.Contains(collision.gameObject.layer))
        {
            collision.gameObject.layer = ObsidianLayerIndex;
            collision.collider.GetComponent<Rigidbody2D>().isKinematic = true;
            collision.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            gameObject.layer = ObsidianLayerIndex;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            converted = true;
        }
    }
}
