using System;

public static class StringTime
{
    public static string SecondsToTimeString(float sec)
    {
        return TimeSpan.FromSeconds(sec).ToString(@"mm\:ss\.ff");
    }
}
