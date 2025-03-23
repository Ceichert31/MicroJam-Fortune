using UnityEngine;
using DG.Tweening;

public class CameraFollowAndShake : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target => GameManager.Instance.PlayerTransform;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 1.0f;
    [SerializeField] private int shakeVibrato = 10;
    [SerializeField] private float shakeRandomness = 90f;
    [SerializeField] private bool fadeOut = true;

    private Vector3 velocity = Vector3.zero;
    private Vector3 originalOffset;
    private bool isShaking = false;

    private void Awake()
    {
        originalOffset = offset;
    }

    private void Update()
    {
        if (!isShaking)
        {
            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime );
        }
        else
        {
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void ShakeCamera(FloatEvent ctx)
    {
        ShakeCamera(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
    }

    private void ShakeCamera(float duration, float strength, int vibrato, float randomness)
    {
        DOTween.Kill(this);

        isShaking = true;

        Vector3 startPosition = transform.position;

        transform.DOShakePosition(duration, strength, vibrato, randomness, false, false).SetId(this).OnComplete(() => 
        {
            isShaking = false;
            offset = originalOffset;
        });
    }
}