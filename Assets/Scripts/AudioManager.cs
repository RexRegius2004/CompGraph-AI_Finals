using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip loseMusic;

    public void PlayMainMenuMusic()
    {
        if (musicSource.clip != mainMenuMusic)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayGameMusic()
    {
        if (musicSource.clip != gameMusic)
        {
            musicSource.clip = gameMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayLoseMusic()
    {
        if (musicSource.clip != loseMusic)
        {
            musicSource.clip = loseMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}

