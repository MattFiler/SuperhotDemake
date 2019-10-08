using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float maxTrailLength = 2;
    LineRenderer lr;

    private float trailLength = 0;
    private bool directionCalculated = false;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        trailLength = Vector3.Distance(transform.position, lr.GetPosition(0));
        if (trailLength > maxTrailLength)
        {
            if(!directionCalculated)
            {
                direction = transform.position - lr.GetPosition(0);
                direction.Normalize();
            }
            lr.SetPosition(0, (transform.position - direction*maxTrailLength));
        }
        lr.SetPosition(1, transform.position);

    }
}
