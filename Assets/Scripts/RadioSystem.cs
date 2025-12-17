using UnityEngine;

public class RadioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] songs;
    private int currentSongIndex = 0;

    void Start()
    {
        PlaySong(currentSongIndex);
    }

    public void PlaySong(int index)
    {
        if (index < 0 || index >= songs.Length) return;
        currentSongIndex = index;
        audioSource.clip = songs[currentSongIndex];
        audioSource.loop = true; // keep looping current track
        audioSource.Play();
    }

    public void NextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        PlaySong(currentSongIndex);
    }

    public void PreviousSong()
    {
        currentSongIndex = (currentSongIndex - 1 + songs.Length) % songs.Length;
        PlaySong(currentSongIndex);
    }

    // 🎚️ Volume control
    public void SetVolume(float volume)
{
    audioSource.volume = volume;
}


}
