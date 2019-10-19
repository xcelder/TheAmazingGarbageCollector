using System;


[Serializable]
public class DebrisContainer
{
    public DebrisData[] debrisData;
}

[Serializable]
public class DebrisData
{
    public string name;
    public float lat1;
    public float lon1;
    public float lat2;
    public float lon2;
    public float alt;
    public float revs_per_day;
    public float size;
}
