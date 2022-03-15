using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageInfoMation
{
    public readonly string StageName;
    public readonly Vector3 EnemySpownPos;
    public readonly Sprite ClearSprite;
    public readonly Sprite LoseSprite;
    public readonly AudioClip Voice;

    public StageInfoMation(string stagename , Sprite clearsplite , Sprite losesprite , AudioClip voice)
    {
        StageName     = stagename;
        EnemySpownPos = GetRandomVector();
        ClearSprite   = clearsplite;
        LoseSprite    = losesprite;
        Voice         = voice;
    }

    private Vector3 GetRandomVector()
    {
        var BasePos = Vector3.zero;
        Vector3 Randompos = Vector3.zero;

        while(Vector3.Magnitude(BasePos - Randompos) < 4.5)
        {
            Randompos = new Vector3(Random.Range(-5.5f, 5.5f) , 0 , Random.Range(-5.5f, 5.5f));
        }
        return Randompos;
    }
}