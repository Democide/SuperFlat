using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] float targetTimeScale = 1f;
    [SerializeField] float timeScaleSpeedGain = 1f;
    [SerializeField] float timeScaleSpeedLoss = 1f;

    bool actedThisFrame;
    bool forceTimeProgress;

    float timeVelocity;
    bool isRunning;

    public float timeElapsed;

    const float minTimescale = 0.08f;
    const float maxTimescale = 0.65f;

#if UNITY_EDITOR
    public float currentTimeScale;
#endif

    public void Init() {
        Time.timeScale = 0f;
    }

    public void StartGame() {
        timeElapsed = 0f;
        targetTimeScale = maxTimescale;
        isRunning = true;
    }

    public void EndGame() {
        isRunning = false;
        Time.timeScale = 0f;
    }

    public void SetMinTimeScale(float minTimeScale) {
        if (this.targetTimeScale < minTimeScale) { 
            this.targetTimeScale = minTimeScale;
        }

        this.actedThisFrame = true;
    }

    public void SetTimeScale(float targetScale = maxTimescale) {
        this.targetTimeScale = targetScale;
        this.actedThisFrame = true;

    }

    public void TimeBoost (float duration, float targetScale = maxTimescale) {
        this.targetTimeScale = targetScale;
        this.forceTimeProgress = true;
        StartCoroutine(CoDisableForceTimeProgress(duration));
    }

    IEnumerator CoDisableForceTimeProgress (float duration) {
        yield return new WaitForSeconds(duration);
        this.forceTimeProgress = false;
    }

    void Update()
    {
        if (!isRunning)
            return;

        if ((actedThisFrame || forceTimeProgress) && Time.timeScale < targetTimeScale) {
            timeVelocity = (timeScaleSpeedGain * Time.unscaledDeltaTime);

            if (Time.timeScale + timeVelocity >= targetTimeScale)
                Time.timeScale = targetTimeScale;
            else
                Time.timeScale += timeVelocity;
        }
        else if ((!actedThisFrame && !forceTimeProgress) && Time.timeScale > minTimescale) {
            timeVelocity = (timeScaleSpeedLoss * Time.unscaledDeltaTime);

            if (Time.timeScale - timeVelocity <= minTimescale)
                Time.timeScale = minTimescale;
            else
                Time.timeScale -= timeVelocity;
        }

        timeElapsed += Time.deltaTime;
        actedThisFrame = false;

#if UNITY_EDITOR
        currentTimeScale = Time.timeScale;
#endif
    }
}
