using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class PenguinClick : MonoBehaviour
{
    public TMP_Text quakText;
    public AudioClip quakClip;   // drag your sound file here
    public float showTime = 1.5f;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        StopAllCoroutines();
        StartCoroutine(ShowQuak());
    }

    IEnumerator ShowQuak()
    {
        quakText.gameObject.SetActive(true);
        quakText.text = "Quak!";
        source.PlayOneShot(quakClip);

        yield return new WaitForSeconds(showTime);
        quakText.gameObject.SetActive(false);
    }
}
