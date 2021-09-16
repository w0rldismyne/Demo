using System;

[System.Serializable]
public class TimeData
{
    public float timePlayed;
    public string date;

    public TimeData() {
        timePlayed = 0;
        date = DateTime.Now.ToString();
    }

    public TimeData(float timePlayed, DateTime dateTime) {
        this.timePlayed = timePlayed;
        date = dateTime.ToString();
    }
}
