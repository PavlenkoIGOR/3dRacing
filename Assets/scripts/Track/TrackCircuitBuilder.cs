using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrackCircuitBuilder
{
    public static TrackPoint[] Build(Transform trackTransform, TrackType type)
    {
        TrackPoint[] trackPoints = new TrackPoint[trackTransform.childCount];

        ResetPoints(trackTransform, trackPoints);

        MakeLinks(trackPoints, type);

        MarkPoint(trackPoints, type);

        return trackPoints;
    }

    static void ResetPoints(Transform trackTransform, TrackPoint[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();

            if (points[i] == null)
            {
                Debug.LogError("err");
                return;
            }

            points[i].Reset();
        }
    }

    static void MakeLinks(TrackPoint[] points, TrackType type)
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i].nextpoint = points[i + 1];
        }

        if (type == TrackType.Circular)
        {
            points[points.Length - 1].nextpoint = points[0];
        }
    }

    static void MarkPoint(TrackPoint[] points, TrackType type)
    {
        points[0].isFirst = true;

        if (type == TrackType.Sprint)
        {
            points[points.Length - 1].isLast = true;
        }

        if (type == TrackType.Circular)
        {
            points[0].isLast = true;
        }
    }
}
