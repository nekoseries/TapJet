using System;

[Serializable]
public class Score
{
    public int jumlah;
    public Data[] data;
}

[Serializable]
public class Data
{
    public string name;
    public int score;
}
