using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    [SerializeField]
    private AudioClip ambianceWestern;

    [SerializeField]
    private AudioClip victoireSpider;

    [SerializeField]
    private AudioClip victoireIndiana;

    [SerializeField]
    private AudioClip victoirePistolet;

    [SerializeField]
    private AudioClip defaitePlayer1;

    [SerializeField]
    private AudioClip defaitePlayer2;

    [SerializeField]
    private AudioClip decompteRound;

    [SerializeField]
    private AudioClip bruitPan;

    [SerializeField]
    private AudioClip bruitSpider;

    [SerializeField]
    private AudioClip bruitLasso;

    [SerializeField]
    private AudioClip annonceRound1;

    [SerializeField]
    private AudioClip annonceRound2;

    [SerializeField]
    private AudioClip annonceRound3;

    [SerializeField]
    private AudioClip test;

    [SerializeField]
    private AudioClip test2;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            PlayAudioOn1("test");
        }

        if (Input.GetKey("up"))
        {
            PlayAudioOn1("test2");
        }
    }

    public void PlayAudioOn1(string clipToPlay)
    {
        switch(clipToPlay)
        {
            case "test":
                Debug.Log("play test 1 sound");
                audioSource1.clip = test;
                audioSource1.Play();
                break;

            case "test2":
                Debug.Log("play test 2 sound");
                audioSource1.clip = test2;
                audioSource1.Play();
                break;

            case "ambianceWestern":
                Debug.Log("ambianceWestern");
                audioSource1.clip = ambianceWestern;
                break;

            case "victoireSpider":
                audioSource1.clip = victoireSpider;
                break;

            case "victoireIndiana":
                audioSource1.clip = victoireIndiana;
                break;

            case "victoirePistolet":
                audioSource1.clip = victoirePistolet;
                break;

            case "defaitePlayer1":
                audioSource1.clip = defaitePlayer1;
                break;

            case "defaitePlayer2":
                audioSource1.clip = defaitePlayer2;
                break;

            case "decompteRound":
                audioSource1.clip = decompteRound;
                break;

            case "bruitPan":
                audioSource1.clip = bruitPan;
                break;

            case "bruitSpider":
                audioSource1.clip = bruitSpider;
                break;

            case "bruitLasso":
                audioSource1.clip = bruitLasso;
                break;

            case "annonceRound1":
                audioSource1.clip = annonceRound1;
                break;

            case "annonceRound2":
                audioSource1.clip = annonceRound2;
                break;

            case "annonceRound3":
                audioSource1.clip = annonceRound3;
                break;

            default:
                Debug.Log("no sound played");
                break;
        }
    }

    public void PlayAudioOn2(string clipToPlay)
    {
        switch (clipToPlay)
        {
            case "base":
                Debug.Log("play base sound");
                audioSource2.Play();
                break;

            case "ambianceWestern":
                Debug.Log("ambianceWestern");
                audioSource2.clip = ambianceWestern;
                break;

            case "victoireSpider":
                audioSource2.clip = victoireSpider;
                break;

            case "victoireIndiana":
                audioSource2.clip = victoireIndiana;
                break;

            case "victoirePistolet":
                audioSource2.clip = victoirePistolet;
                break;

            case "defaitePlayer1":
                audioSource2.clip = defaitePlayer1;
                break;

            case "defaitePlayer2":
                audioSource2.clip = defaitePlayer2;
                break;

            case "decompteRound":
                audioSource2.clip = decompteRound;
                break;

            case "bruitPan":
                audioSource2.clip = bruitPan;
                break;

            case "bruitSpider":
                audioSource2.clip = bruitSpider;
                break;

            case "bruitLasso":
                audioSource2.clip = bruitLasso;
                break;

            case "annonceRound1":
                audioSource2.clip = annonceRound1;
                break;

            case "annonceRound2":
                audioSource2.clip = annonceRound2;
                break;

            case "annonceRound3":
                audioSource2.clip = annonceRound3;
                break;

            default:
                Debug.Log("no sound played");
                break;
        }
    }
}
