using UnityEngine;

namespace Infrastructure
{
    public interface IAudioSystem
    {
        void Play(AudioOutput output, string clipName, float volumeScale = 1f);
        void PlayOneShot(AudioOutput output, string clipName, float volumeScale = 1f, float pitch = 1f);
        void Play(AudioOutput output, string clipName, Vector3 position, float volumeScale = 1f);
        void Play(AudioOutput output, string clipName, Transform parent, float volumeScale = 1f);
    }
}