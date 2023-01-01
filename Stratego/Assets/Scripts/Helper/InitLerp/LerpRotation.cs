using System;
using System.Collections;
using UnityEngine;

public class LerpRotation: BaseEventCallback
{
    private float previousAngleDiff = 0;

    public void RotateTowardsDestination(Vector3 endPosition, float delayedStart = 0, float rotationSpeed = 1, bool onlyRotateOnYAs = true, Action callbackOnFinished = null, bool destroyGoOnFinished = false)
    {
        StopAllCoroutines();
        StartCoroutine(RotateTowardsDestinationLerp(endPosition, delayedStart, rotationSpeed, onlyRotateOnYAs, callbackOnFinished, destroyGoOnFinished));
    }

    private IEnumerator RotateTowardsDestinationLerp(Vector3 endPosition, float delayedStart, float rotationSpeed, bool onlyRotateOnYAs, Action callbackOnFinished, bool destroyGoOnFinished)
    {
        float elapsedTime = 0f;
        var targetDirection = endPosition - transform.position;

        yield return Wait4Seconds.Get(delayedStart);

        while (elapsedTime < 3)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 4 * rotationSpeed * Time.deltaTime, 0.0f);

            var currentAngleDiff = Vector3.Angle(newDirection, targetDirection);
            if (Math.Abs(currentAngleDiff - previousAngleDiff) < 0.01)
            {
                break;
            }

            var oldRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.LookRotation(newDirection);
            if(onlyRotateOnYAs)
            {
                var onlyYRotation = new Vector3(oldRotation.x, transform.rotation.eulerAngles.y, oldRotation.z);
                transform.rotation = Quaternion.Euler(onlyYRotation);
            }

            previousAngleDiff = currentAngleDiff;
            yield return null;
        }

        callbackOnFinished?.Invoke();
        if (destroyGoOnFinished) { Destroy(gameObject); }
    }

    public void RotateTowardsAngle(Quaternion endRotation, float duration, Quaternion? startRotation = null, float delayedStart = 0, Action callbackOnFinished = null, bool destroyGoOnFinished = false)
    {
        StopAllCoroutines();
        StartCoroutine(RotateTowardsAngleLerp(endRotation, duration, startRotation, delayedStart, callbackOnFinished, destroyGoOnFinished));
    }

    private IEnumerator RotateTowardsAngleLerp(Quaternion endRotation, float duration, Quaternion? startRotation, float delayedStart, Action callbackOnFinished, bool destroyGoOnFinished)
    {        
        yield return Wait4Seconds.Get(delayedStart);

        Quaternion startPos = startRotation.HasValue ? startRotation.Value : transform.rotation;
        transform.rotation = startPos;

        float elapsedTime = 0f;
        var curve = MonoHelper.instance.CurveGradual;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percComplete = elapsedTime / duration;
            transform.rotation = Quaternion.Lerp(startPos, endRotation, percComplete);
            yield return null;
        }

        callbackOnFinished?.Invoke();
        if (destroyGoOnFinished) { Destroy(gameObject); }
    }
}

