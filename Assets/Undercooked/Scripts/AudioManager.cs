using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    public string title;
    public AudioClip audioClip;
    public float volume = 1;
    public bool loop;
}
public class AudioManager : MonoBehaviour
{

    public List<AudioData> audioDatas = new List<AudioData>();

    public List<AudioSource> audioSources = new List<AudioSource>();


    public void PlayClip(int clipNum)
    {
        InitAudio(clipNum);
    }

    void InitAudio(int clipNum)
    {
        AudioData audioData = GetAudioData(clipNum);

        if (audioData != null)
        {
            // Vi finder (eller laver) en ledig audioSource p� objektet
            // Alle indstillingerne vi har valgt til audioclippet, bliver sat her. (Dette er langt nemmere end hvis vi bruger et r� audioclip og janky ting, stol p� mig bro)
            // Og s� kan audioclippet blive afspillet
            AudioSource audioSource = GetAudioSource();
            audioSource.clip = audioData.audioClip;
            audioSource.volume = audioData.volume;
            audioSource.loop = audioData.loop;
            audioSource.Play();

        }
        else
        {
            Debug.Log("No audio data with the name " + clipNum);
        }


    }

    // Her opsamler vi det audioclip + data der passer til den plads i listen vi har sendt ind (S� husk at s�tte alle audioclips i korrekt r�kkef�lge)
    AudioData GetAudioData(int clipNum)
    {
        AudioData audioData = audioDatas[clipNum];

        return audioData;
    }

    // Her tjekker vi for en sikkerheds skyld om der er en audiosource ledig i gameobjektet til at spille den n�ste lyd
    // Og hvis der ikke er, genererer den en ny audioSource
    // ((Den del burde ikke bliver super relevant, da eventManager allerede tjekker om en forrige lyd er f�rdig med at afspille f�r den kalder p� den n�ste, men man kan aldrig v�re for sikker ig))
    // Den returnere derefter audiosourcen, s� InitAudio har noget at spille det n�ste audioClip med.
    AudioSource GetAudioSource()
    {

        AudioSource audioSource = audioSources.Find(nextAudioSource => !nextAudioSource.isPlaying);
        if (audioSource != null)
        {
            return audioSource;

        }
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
            return audioSource;
        }

    }

    public void StopAudio(int clipNum)
    {
        AudioData audioData = GetAudioData(clipNum);

        if (audioData != null)
        {
            AudioSource audioSource = audioSources.Find(nextAudioSource => nextAudioSource.clip == audioData.audioClip);
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
