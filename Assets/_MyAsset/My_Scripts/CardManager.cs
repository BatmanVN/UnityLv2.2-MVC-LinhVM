using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private List<Card> cardAdd = null;
    [SerializeField] private List<Card> cards;
    [SerializeField] private AudioSource[] audios;
    [SerializeField] private GameObject loseBanner;
    [SerializeField] private GameObject[] vfxStatus;
    [SerializeField] private float timeDestroy;
    [SerializeField] private float timeClear;
    [SerializeField] private float time;
    [SerializeField] private float index;
    public UnityEvent onWin;
    private bool issucced;

    public float Index { get => index; set => index = value; }

    private void Awake()
    {
        Stop();
    }
    private void Stop()
    {
        Time.timeScale = 0;
    }
    public void Play()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        //StartCoroutine(CheckIndex());
        //while (time > 0)
        //{
        //    time -= Time.deltaTime;
        //    if(time<=0)
        //        ResetCard();
        //}
    }
    public void AddCard(Card card)
    {
        if (cardAdd.Count >= 2)
            return;
        if (cardAdd.Count < 2)
        {
            time = 3f;
            cardAdd.Add(card);
        }
        if (cardAdd.Count == 2)
        {
            DisableAllCard();
            StartCoroutine(CheckDelay());
        }
    }
    private void DisableAllCard()
    {
        foreach (Card card in FindObjectsOfType<Card>())
        {
            card.DisableButton();
        }
    }
    private void EnableAllCard()
    {
        foreach (Card card in FindObjectsOfType<Card>())
        {
            card.EnableButton();
        }
    }
    private IEnumerator CheckDelay()
    {
        CheckCard();
        yield return new WaitForSeconds(timeClear);
        EnableAllCard();
    }
    private void CheckCard()
    {
        if (cardAdd[0] != null && cardAdd[1] != null)
        {
            if (cardAdd[0].NameCard == cardAdd[1].NameCard)
            {
                cardAdd[0].Disable(timeDestroy);
                cardAdd[1].Disable(timeDestroy);
                vfxStatus[0].SetActive(true);
                Invoke(nameof(DisVFXSucces), 1f);
                Invoke(nameof(ClearListCard), timeClear);
                issucced = true;
            }
            if (cardAdd[0].NameCard != cardAdd[1].NameCard)
            {
                timeClear = 4f;
                Invoke(nameof(ClearListCard), timeClear);
                vfxStatus[1].SetActive(true);
                Invoke(nameof(DisplayVFXFail), 1f);
                Index -= 1;
                issucced = false;
            }
        }
        PlayAudio();
    }
    private void DisVFXSucces()
    {
        vfxStatus[0].SetActive(false);
    }
    private void DisplayVFXFail()
    {
        vfxStatus[1].SetActive(false);
    }
    private IEnumerator EnableFailBar()
    {
        yield return new WaitForSeconds(1f);
        loseBanner.SetActive(Index < -3);
    }
    private void EnableSuccessbar()
    {
        onWin?.Invoke();
    }
    private void PlayAudio()
    {
        foreach (AudioSource audio in audios)
        {
            if (issucced && audio.name == "Success")
            {
                audio.Play();
            }
            if (!issucced && audio.name == "Fail")
            {
                audio.Play();
            }
        }
    }

    private void ClearListCard()
    {
        cardAdd.Clear();
    }

    // for flip again card if player just click 1 card and time Disable in class card <=0
    private void ResetCard()
    {
        if (cardAdd.Count == 1)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                ClearListCard();
            }
        }
    }
    public void RestFail(float count)
    {
        Index = count;
    }
    private void Update()
    {
        ResetCard();
        StartCoroutine(EnableFailBar());
        EnableSuccessbar();
    }
}
