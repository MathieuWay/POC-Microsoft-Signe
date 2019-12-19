using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerRewind : Step
{
    public Rewind rewind;
    public RewindVideo rewindVideo;
    public PlayableDirector director;


    public override void StartGameflow()
    {
        rewind.ToggleScene();
        if (rewindVideo)
            rewindVideo.ToggleScene();
        director.stopped += OnPlayableDirectorEnded;
    }

    public void OnPlayableDirectorEnded(PlayableDirector director)
    {
        GameflowManager.Instance.CompletedTutorial();
        Debug.Log("marche");
    }
}
