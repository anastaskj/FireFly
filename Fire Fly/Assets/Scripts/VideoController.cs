using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    //public VideoPlayer player;
    //public VideoClip clip;
    //public AudioSource audioClip;
    //public RawImage image;

    //// Use this for initialization
    //void Start()
    //{
    //    audioClip.Play();
    //    player.loopPointReached += CheckOver;
    //}

    //void LateUpdate()
    //{
    //    if (Input.anyKeyDown)
    //    {
    //        player.Stop();
    //        audioClip.Stop();
    //        Destroy(image);
    //    }

    //}

    //void CheckOver(VideoPlayer vp)
    //{
    //    audioClip.Stop();
    //    Destroy(image);
    //}


    //Raw Image to Show Video Images [Assign from the Editor]
    public RawImage image;
    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;

    public VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //Audio

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());

        videoPlayer.loopPointReached += CheckOver;
    }

    IEnumerator playVideo()
    {
       


        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            
            yield return null;
        }
       

        //Play Video
        videoPlayer.Play();

        //Play Sound
        //audioSource.Play();

        
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        
    }

    void LateUpdate()
    {
        if (Input.anyKeyDown)
        {
            videoPlayer.Stop();
            Destroy(image);
        }

    }

    void CheckOver(VideoPlayer vp)
    {
        videoPlayer.Stop();
        Destroy(image);
    }
}
