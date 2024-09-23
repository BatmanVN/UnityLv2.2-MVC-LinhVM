using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private List<Card> cardAdd = null;
    [SerializeField] private AudioSource[] audios;
    [SerializeField] private GameObject loseBanner;
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
        
    }
    public void AddCard(Card card)
    {
        if (cardAdd.Count >= 2)
            return;
        if (cardAdd.Count < 2)
        {
            time = 4f; //set up again time everytime add a new card
            cardAdd.Add(card);
        }
        if (cardAdd.Count == 2)
        {
            DisableAllCard(); //Every time List card Selected = 2 will disable all button
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
        CheckCard(); // check card
        yield return new WaitForSeconds(timeClear); // then wait timeClear setup follow CheckCard
        EnableAllCard(); //Enable all button if Finsihed check card
    }
    private void CheckCard()
    {
        if (cardAdd[0] != null && cardAdd[1] != null)
        {
            if (cardAdd[0].NameCard == cardAdd[1].NameCard)
            {
                cardAdd[0].Disable(timeDestroy);
                cardAdd[1].Disable(timeDestroy);
                Invoke(nameof(ClearListCard), timeClear); // if 2card same timeclear = setup(1,1.2,2s...)
                issucced = true;
            }
            if (cardAdd[0].NameCard != cardAdd[1].NameCard)
            {
                timeClear = 4f;  // if 2card diff timeclear = time disableSprite in class card
                Invoke(nameof(ClearListCard), timeClear);
                Index -= 1;
                issucced = false;
            }
        }
        PlayAudio();
    }
    private void EnableFailBar()
    {
        loseBanner.SetActive(Index <= -3);
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
    private void ResetCard() // for flip again card if player just click 1 card and time Disable in class card <=0
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
        EnableFailBar();
        EnableSuccessbar();
    }
}
