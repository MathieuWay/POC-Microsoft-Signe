using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerDirector : Step
{
    public PlayableDirector director;


    public override void StartGameflow()
    {
        director.Play();
        director.stopped += onPlayableDirectorEnded;
    }

    public void onPlayableDirectorEnded(PlayableDirector director)
    {
        GameflowManager.Instance.CompletedTutorial();
    }
}
