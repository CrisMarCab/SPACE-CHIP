using UnityEngine;

public class Beyond : MonoBehaviour
{
    [SerializeField]
    AudioSource lastVoice, music, spacechipSound, spacechipSound2, masAlla;
    bool cameThrough;

    private void Update()
    {

        if (cameThrough)
        {
            music.volume = music.volume - 0.01f;
            spacechipSound.volume = spacechipSound.volume - 0.01f;
            spacechipSound2.volume = spacechipSound2.volume - 0.01f;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (cameThrough == false)
        {
            lastVoice.Play();
        }

        cameThrough = true;

    }
}
