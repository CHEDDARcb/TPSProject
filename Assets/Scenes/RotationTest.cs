using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public Transform targetTransform;
    private Vector3 target;
    private Vector3 targetDir;
    // Start is called before the first frame update
    void Start()
    {
        target = targetTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(target.normalized);
        
    }

    private void Update()
    {
        target = targetTransform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, 1 * Time.deltaTime);
        Debug.Log(transform.position.magnitude / targetTransform.position.magnitude);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, transform.position.magnitude / targetTransform.position.magnitude);
    }
}
