using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class UiManager : MonoBehaviour{    public float Seconds;    private int mScore = 0;    private float mTimer = 0;    private Text mScoreText;    private Text mTimerText;    public void Start()    {        Transform scoreTransform = transform.Find("Score/scoreActual");        mScoreText = scoreTransform.GetComponent<Text>();        UpdateScore(0);        Transform timerTransform = transform.Find("timer/Text");        mTimerText = timerTransform.GetComponent<Text>();    }    public void UpdateScore(int delta)    {        if (IsGameOver())
        {
            return;
        }        mScore += delta;        mScoreText.text = string.Format("{0:D5}", mScore);    }    public void Update()    {        mTimer += Time.deltaTime;        float secondsRemaining = Mathf.Max(Seconds - mTimer, 0.0f);        mTimerText.text = string.Format("{0:D2}:{1:D2}", (int)secondsRemaining, (int)(secondsRemaining * 100) % 100);        if (IsGameOver())
        {
            mTimerText.color = Color.red;
            mScoreText.color = Color.red;        }    }    private bool IsGameOver()
    {
        return mTimer >= Seconds;
    }}