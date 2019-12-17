using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerRewindVideo : Step
{
    public RewindVideo rewindVideo;
    public PlayableDirector director;


    public override void StartGameflow()
    {
        rewindVideo.ToggleScene();
        director.stopped += onPlayableDirectorEnded;
    }

    public void onPlayableDirectorEnded(PlayableDirector director)
    {
        GameflowManager.Instance.CompletedTutorial();
    }
}
