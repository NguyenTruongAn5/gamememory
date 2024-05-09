using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ScoreBoard : MonoBehaviour
{
    public Text[] scoreText_10Pairs;
    public Text[] dateText_10Pairs;

    public Text[] scoreText_15Pairs;
    public Text[] dateText_15Pairs;

    public Text[] scoreText_20Pairs;
    public Text[] dateText_20Pairs;
    void Start()
    {
        UpdateScoreBoard();
    }

    public void UpdateScoreBoard()
    {
        Config.UpdateScoreList();
        DisplayPairScoreData(Config.ScoreTimeList10Pairs, Config.PairNumberList10Pairs, scoreText_10Pairs, dateText_10Pairs);
        DisplayPairScoreData(Config.ScoreTimeList15Pairs, Config.PairNumberList15Pairs, scoreText_15Pairs, dateText_15Pairs);
        DisplayPairScoreData(Config.ScoreTimeList20Pairs, Config.PairNumberList20Pairs, scoreText_20Pairs, dateText_20Pairs);
    }

    private void DisplayPairScoreData(float[] scoreTimeList, string[] pairNumberList, Text[] scoreText, Text[] dataText)
    {
        for(int index = 0; index < 3; index++)
        {
            if (scoreTimeList[index] > 0)
            {
                var dataTime = Regex.Split(pairNumberList[index], "T");

                var minus = Mathf.Floor(scoreTimeList[index]/60);
                float second = Mathf.RoundToInt(scoreTimeList[index]%60);

                scoreText[index].text = minus.ToString("00") + ":" + second.ToString("00");
                dataText[index].text = dataTime[0] + " " + dataTime[1];
            }
            else
            {
                scoreText[index].text = " ";
                dataText[index].text = " ";
            }
        }
    }
}
