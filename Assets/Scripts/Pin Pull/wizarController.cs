using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizarController : MonoBehaviour
{
    public List<Transform> navPoints;
    int navTarget = 0;
    [SerializeField] private float navThreshold = 0.2f;
    public float moveSpeed = 5f;
    private Animator anim;

    private void Start()
    {
        //anim = GetComponent<Animator>();
    }


    void Update()
    {
        float distance = Vector2.Distance(navPoints[navTarget].position, transform.position);
        if (distance > navThreshold)
        {
            Vector2 dir = (navPoints[navTarget].position - transform.position) / distance;
            transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;
        }
        else
            anim?.SetBool("Moving", false);
    }

    public void Move()
    {
        navTarget++;
        anim?.SetBool("Moving", true);
    }

    public void PopNavPoint(Transform navPoint)
    {
        navPoints.Remove(navPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, navPoints[0].position);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(navPoints[0].position, 0.5f);
        for (int i = 1; i < navPoints.Count; i ++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(navPoints[i].position, 0.5f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(navPoints[i - 1].position, navPoints[i].position);
        }
    }
}
