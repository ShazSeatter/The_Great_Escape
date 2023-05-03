using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip sfxToPlay;
    public float volume = 1f;

    // 3 times we can play sfx
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    // Delayed sound timer
    public float playDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnEnter)
        {
            // (sound, playing the clip of character at a position, and then volume)
            AudioSource.PlayClipAtPoint(sfxToPlay, animator.gameObject.transform.position, volume);
        }
        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            if(timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(sfxToPlay, animator.gameObject.transform.position, volume);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            // (sound, playing the clip of character at a position, and then volume)
            AudioSource.PlayClipAtPoint(sfxToPlay, animator.gameObject.transform.position, volume);
        }
    }


}
