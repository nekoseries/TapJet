using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{

    [SerializeField] private GameObject[] scorePanel;
    [SerializeField] private TextMeshProUGUI[] nameText;
    [SerializeField] private TextMeshProUGUI[] scoreText;
    
    
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private SQLConnector _sqlConnector;

    public void RenderScore(Score score)
    {
        title.text = "10 top Score";

        for (int i = 0; i < 10; i++)
        {
            scorePanel[i].SetActive(true);
            if (i < score.jumlah)
            {
                RenderPanelScore(score.data[i], i);
            }
            else
            {
                RenderPanelScore(null, i);
            }
        }
    }

    public void CloseUI()
    {
        _sqlConnector.StopAllCoroutines();
        for (int i = 0; i < 10; i++)
        {
            scorePanel[i].SetActive(false);
        }
    }

    private void RenderPanelScore(Data data, int index)
    {
        if (data != null)
        {
            nameText[index].text = data.name;
            scoreText[index].text = "" + data.score;
        }
        else
        {
            nameText[index].text = "-";
            scoreText[index].text = "0";
        }
    }

    public void RenderScore(string score)
    {
        title.text = score;
    }
}
