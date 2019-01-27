using UnityEngine;

public class Beyond : MonoBehaviour
{
    [SerializeField]
    AudioSource lastVoice, music, spacechipSound, spacechipSound2;


    private void OnTriggerEnter(Collider other)
    {
        lastVoice.Play();
        music.Stop();

        spacechipSound.volume = 0;
        spacechipSound2.volume = 0;
    }
}
