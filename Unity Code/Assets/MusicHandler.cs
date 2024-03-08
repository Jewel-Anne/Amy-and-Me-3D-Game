using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    public AudioSource startScreenMusic;
    public AudioSource playgroundMusic;
    public AudioSource tieVictoryMusic;

    private AudioSource activeMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        activeMusic = startScreenMusic; // Set the initial active music source
        PlayMusic(activeMusic);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Pause the inactive music sources
        PauseMusic(startScreenMusic);
        PauseMusic(playgroundMusic);
        PauseMusic(tieVictoryMusic);

        // Check the scene name and assign the appropriate music source
        switch (scene.name)
        {
            case "Start Screen":
                activeMusic = startScreenMusic;
                break;
            case "PlaygroundGameFinal":
            case "Level 2":
            case "Level 3":
                activeMusic = playgroundMusic;
                break;
            case "Defeat":
            case "Tie":
            case "Victory":
                activeMusic = tieVictoryMusic;
                break;
            default:
                activeMusic = null;
                break;
        }

        // Resume playing the active music source
        PlayMusic(activeMusic);
    }

    private void PlayMusic(AudioSource audioSource)
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void PauseMusic(AudioSource audioSource)
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}
