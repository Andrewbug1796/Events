using System.Collections;
using UnityEngine;

public class CameraShakeOnFire : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float shakeDuration = 0.1f;
    [SerializeField] float shakeMagnitude = 0.05f;

    Vector3 originalLocalPosition;

    void Start()
    {
        if (gun != null)
        {
            gun.OnGunFired.AddListener(ShakeCamera);
        }

        if (cameraTransform != null)
        {
            originalLocalPosition = cameraTransform.localPosition;
        }
    }

    void ShakeCamera()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);

            cameraTransform.localPosition = originalLocalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalLocalPosition;
    }
}