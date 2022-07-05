using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;


public class ShowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //private TextMeshProUGUI text;
    public Transform ElementToShow;
    public AudioClip[] SoundsToPlay;
    public AudioMixerGroup MixerGroup;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ElementToShow.gameObject.SetActive(true);
        if (SoundsToPlay.Length >0)
        {
            int soundIndex = Random.Range(0, SoundsToPlay.Length);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = SoundsToPlay[soundIndex];
            audioSource.outputAudioMixerGroup = MixerGroup;
            audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ElementToShow.gameObject.SetActive(false);
    }
     public void OnPointerClick(PointerEventData eventData)
    {
        ElementToShow.gameObject.SetActive(false);
    }
}
