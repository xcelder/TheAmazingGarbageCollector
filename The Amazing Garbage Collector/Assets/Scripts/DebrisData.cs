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
    public float lat;
    public float lon;
    public float alt;
}
