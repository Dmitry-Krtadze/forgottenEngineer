using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numSegments = 10;
    public float maxOffset = 0.1f;
    public float speed = 1f;

    private Vector3[] originalPositions;
    private float[] offsets;

    void Start()
    {
        originalPositions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(originalPositions);

        offsets = new float[lineRenderer.positionCount];
        for (int i = 0; i < offsets.Length; i++)
        {
            offsets[i] = Random.Range(-maxOffset, maxOffset);
        }
    }

    void Update()
    {
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 newPosition = originalPositions[i] + new Vector3(0, Mathf.Sin(Time.time * speed + i) * offsets[i], 0);
            lineRenderer.SetPosition(i, newPosition);
        }
    }
}
