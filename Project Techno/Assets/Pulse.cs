using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour {

    [SerializeField]
    float speedOfPing = 2.0f;
    [SerializeField]
    int numVerts = 40;
    [SerializeField]
    private float pulseDistance = 15f;
    [SerializeField]
    private Material mat;
    [SerializeField]
    private float width = 0.5f;
    [SerializeField]
    private LayerMask ignoreLayer;
    [SerializeField]
    private bool usePulseOrigin = false;

    IEnumerator pulseRoutine = null;
    LineRenderer lr;
    float radius = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pulseRoutine != null)
            {
                Destroy(lr);
                StopCoroutine(pulseRoutine);
            }
            pulseRoutine = SendPulse();
            StartCoroutine(pulseRoutine);
        }
    }

    IEnumerator SendPulse()
    {
        Destroy(lr);
        yield return null;
        lr = gameObject.AddComponent<LineRenderer>();
        Vector3 origin = transform.position;

        lr.material = mat;

        
        lr.positionCount = (numVerts + 1);
        lr.useWorldSpace = true;
        lr.loop = true;

        lr.startWidth = lr.endWidth = width;
        for (radius = 0;
                radius < pulseDistance;
                radius += speedOfPing * Time.deltaTime)
        {
            lr.material.SetFloat("_ChangePoint", radius - .5f);
            Vector3 pulseOrigin = usePulseOrigin ? origin : transform.position;
            lr.material.SetVector("_CentrePoint", pulseOrigin);
            for (int vertno = 0; vertno <= numVerts; vertno++)
            {
                float angle = (vertno * Mathf.PI * 2) / numVerts;
                
                Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, .5f, Mathf.Sin(angle) * radius);
                RaycastHit hit;
                if (Physics.Raycast(pulseOrigin, (pos).normalized, out hit, radius, ~ignoreLayer))
                    pos = hit.point;
                else
                    pos += pulseOrigin;
                lr.SetPosition(vertno, (pos));
            }
            yield return null;
        }
        pulseRoutine = null;
        //Destroy(lr);
    }

    private void OnDrawGizmos()
    {
        for (int vertno = 0; vertno <= numVerts; vertno++)
        {
            float angle = (vertno * Mathf.PI * 2) / numVerts;
            Vector3 pulseOrigin = transform.position;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * radius, .5f, Mathf.Sin(angle) * radius);

            Gizmos.color = Color.green;
            RaycastHit hit;
            if (Physics.Raycast(pulseOrigin, (pos).normalized, out hit, radius, ~ignoreLayer))
            {
                pos = hit.point;
                Gizmos.color = Color.red;
            }
            else
            {
                pos = pulseOrigin + pos;
            }
            Gizmos.DrawLine(pulseOrigin, pos);
        }
    }
}
