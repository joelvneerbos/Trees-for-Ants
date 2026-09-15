using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public enum ScoreType { Apples, Oranges }

    public TextMeshProUGUI text;

    private static int _applesEaten = 0;
    private static int _orangesEaten = 0;

    private bool finished = false;

    void Update()
    {
        if (finished) { return; }

        int secondsRemaining = Mathf.FloorToInt(300f - Time.time);

        int applesScore = _applesEaten * 10;
        int orangesScore = _orangesEaten * 10;
        int bonusScore = Mathf.Min(_applesEaten, _orangesEaten) * 20;
        int totalScore = applesScore + orangesScore + bonusScore;

        text.text = $"Resterende tijd: {secondsRemaining / 60}:{secondsRemaining % 60:00}\n\n"
            + $"<sprite name=apple> x {_applesEaten} = {applesScore}\n"
            + $"<sprite name=orange> x {_orangesEaten} = {orangesScore}\n"
            + $"<sprite name=apple> & <sprite name=orange> x {Mathf.Min(_applesEaten, _orangesEaten)} = {bonusScore}\n\n"
            + $"Totale score: {totalScore}";

        finished = secondsRemaining <= 0;
    }

    public static void IncrementScore(ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.Apples: _applesEaten++; return;
            case ScoreType.Oranges: _orangesEaten++; return;
        }
    }
}
