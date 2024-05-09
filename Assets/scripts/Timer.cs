using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GUIStyle ClockStyle;

    private float _timer;
    private float _minutes;
    private float _seconds;

    public Image timeBar;
    float curtime = 70;
    public GameObject GameOverPannel;


    private const float virtalWidth = 480.0f;
    private const float virtualHeight = 854.0f;

    private bool _stoptimer;
    private Matrix4x4 _matrix;
    private Matrix4x4 _oldMatrix;

    // Start is called before the first frame update
    void Start()
    {
        _stoptimer = false;
        _matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / virtalWidth, Screen.height/virtualHeight, 1.0f));
        _oldMatrix = GUI.matrix;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stoptimer)
            _timer += Time.deltaTime;
        curtime -= Time.deltaTime;
        UpdateTimeBar(curtime, 70);
        if(curtime <= 0)
        {
            GameOverPannel.SetActive(true);
        }
    }

    public void UpdateTimeBar(float curTime, float totalTime)
    {
        float rate = curTime / totalTime;
        if (timeBar)
        {
            timeBar.fillAmount = rate;
        }
    }
    private void OnGUI()
    {
        GUI.matrix= _matrix;
        _minutes = Mathf.Floor( _timer/ 60 );
        _seconds = Mathf.RoundToInt( _timer% 60 );
        GUI.Label(new Rect(Camera.main.rect.x + 20, 10, 120, 50), "" +_minutes.ToString("00") + ":" + _seconds.ToString("00"), ClockStyle);
        GUI.matrix= _oldMatrix;
    }

    public float GetCurrentTimer()
    {
        return _timer;
    }
    public void StopTimer()
    {
        _stoptimer = true;
    }
}
