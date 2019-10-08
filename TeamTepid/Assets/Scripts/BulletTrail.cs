using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float maxTrailLength = 2;
    LineRenderer lr;

    private float trailLength = 0;
    private Vector3 addZ = new Vector3(0, 0, 1);
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position + addZ);
    }

    // Update is called once per frame
    void Update()
    {
        trailLength = Vector3.Distance(transform.position, lr.GetPosition(0));
        if (trailLength > maxTrailLength)
        {
            Vector3 direction = transform.position - lr.GetPosition(0);
            direction.Normalize();
            lr.SetPosition(0, (transform.position - direction) + addZ);
        }
        lr.SetPosition(1, transform.position + addZ);

    }
}
