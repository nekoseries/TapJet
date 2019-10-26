using System.Collections;
using UnityEngine;

public class SQLConnector : MonoBehaviour
{
    
    private string displayscoreURL = "http://ariutomo.tech/display.php";
    private string updatescoreURL = "http://ariutomo.tech/updatescore.php";

    private string tempScores = "";

    [SerializeField] private UIScore UIScore;
    
    
    // This is for debugging purposes, you can run this when clicking on 
    // a button, to see the highscores that have been added. Remove when done setting up.
    public void GetTheScores()
    {
        StartCoroutine(GetScores());
    }
    
    //This is where we post 
    public void PostScores(string name, int score, string secretkey)
    {
        string hash = Encrypt.Md5Sum(name + score + secretkey);
        WWWForm form = new WWWForm();
        form.AddField("namePost", name);
        form.AddField("scorePost", score);
        form.AddField("secretkeyPost", secretkey);
        form.AddField("hashPost", hash);
        WWW www = new WWW(updatescoreURL, form);
    }
    
    //This co-rutine gets the score, and print it to a text UI element.
    IEnumerator GetScores()
    {
        WWW wwwHighscores = new WWW(displayscoreURL);
        while (!wwwHighscores.isDone)
        {
            UIScore.RenderScore("Loading...");
            yield return null;    
        }
        
        if (wwwHighscores.error != null)
        {
            print("There was an error getting the high score: " + wwwHighscores.error);
            UIScore.RenderScore("Error Connection");
        }
        else
        {
            UIScore.RenderScore(JsonUtility.FromJson<Score>(wwwHighscores.text));
        }
    }
}

public class Encrypt
{
    // This is used to create a md5sum - so that we are sure that only legit scores are submitted.
    // We use this when we post the scores.
    // This should probably be placed in a seperate class. But isplaced here to make it simple to understand.
    public static string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);
        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 =
            new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);
        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
