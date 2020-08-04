using UnityEngine;

public class RotateAndBounce : MonoBehaviour
{
    [SerializeField]
    private float bounceHeight = 0.2f;

    [SerializeField]
    [Tooltip("Time in seconds for one full bounce.")]
    private float bounceSpeed = 1.5f;

    [SerializeField]
    [Tooltip("Time in seconds for one full rotation")]
    private float rotationSpeed = 1.5f;

    private Transform goTransform = null;

    private float timeSum = 0.0f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Awake()
    {
        goTransform = gameObject.transform;

        startPosition = goTransform.localPosition;
        endPosition = startPosition + new Vector3(0.0f, bounceHeight, 0.0f);

    }

    private void Update()
    {
        float rotationDelta = (360f / rotationSpeed) * Time.deltaTime;
        float bounceAlpha = timeSum / bounceSpeed;


        goTransform.rotation = Quaternion.AngleAxis(rotationDelta, Vector3.up) * goTransform.rotation;

        goTransform.localPosition = Vector3.Lerp(startPosition, endPosition, Mathf.Sin(bounceAlpha) * Mathf.Sin(bounceAlpha));

        timeSum += Time.deltaTime;
    }
}
