using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] float targetTimeScale = 1f;
    [SerializeField] float timeScaleSpeed = 1f;

    float timeVelocity;
    bool isRunning;

    public float timeElapsed;

#if UNITY_EDITOR
    public float currentTimeScale;
#endif

    public void Init() {
    }

    public void StartGame() {
        timeElapsed = 0f;
        isRunning = true;
    }

    public void EndGame() {
        isRunning = false;
    }

    public void SetTimeScale(float targetScale) {
        this.targetTimeScale = targetScale;
    }

    void Update()
    {
        if (!isRunning)
            return;

        if (Time.timeScale < targetTimeScale) {
            timeVelocity = (timeScaleSpeed * Time.unscaledDeltaTime);

            if (Time.timeScale + timeVelocity >= targetTimeScale)
                Time.timeScale = targetTimeScale;
            else
                Time.timeScale += timeVelocity;
        }
        else if (Time.timeScale > targetTimeScale) {
            timeVelocity = (-timeScaleSpeed * Time.unscaledDeltaTime);

            if (Time.timeScale + timeVelocity <= targetTimeScale)
                Time.timeScale = targetTimeScale;
            else
                Time.timeScale += timeVelocity;
        }

        timeElapsed += Time.deltaTime;

#if UNITY_EDITOR
        currentTimeScale = Time.timeScale;
#endif
    }
}
