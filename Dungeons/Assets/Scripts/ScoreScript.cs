using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI level_text;
    public TextMeshProUGUI trans_level;

    public TextMeshProUGUI go_level;
    public TextMeshProUGUI go_score;

    public int score;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        score = 0000000;
        score_text.text = score.ToString("0000000");

        level = 1;
        level_text.text = level.ToString();
    }

    private void Update()
    {
        score_text.text = score.ToString("0000000");
        level_text.text = level.ToString();
        trans_level.text = level.ToString("0");

        go_level.text = level.ToString();
        go_score.text = score.ToString("0000000");
    }
}
