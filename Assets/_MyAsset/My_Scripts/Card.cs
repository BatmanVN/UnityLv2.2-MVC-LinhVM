using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private const string flipParaname = "flip";
    [SerializeField] private string nameCard;
    [SerializeField] private Animator cardAnim;
    [SerializeField] private GameObject[] img;
    [SerializeField] private Image image;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private AudioSource cardSound;
    [SerializeField] private Button buton;
    [SerializeField] private float time;
    [SerializeField] private bool isClick;
    [SerializeField] private UnityEvent onClick;
    [SerializeField] private UnityEvent onSwap;
    
    public string NameCard { get => nameCard; }

    private void Start()
    {
        ShowCard();
    }
    private void ShowCard()
    {
        EnableFlip();
        Invoke(nameof(DisableFlip), 1.5f);
        Invoke(nameof(Swap), 2f);
    }
    private void Swap()
    {
        cardAnim.SetTrigger("swap");
        onSwap?.Invoke();
    }

    public void EnableFlip()
    {
        img[1].SetActive(true);
    }
    public void DisableFlip()
    {
        img[1].SetActive(false);
    }
    private void FlipCard()
    {
        cardAnim.SetBool(flipParaname, true);
        ChangeSprite();
        SetTime();
    }
    private void ChangeSprite()
    {
        nameCard = image.sprite.name;
    }
    private void DisAbleSprite()
    {
        cardAnim.SetBool(flipParaname, false);
    }
    private void SetTime()
    {
        time -= UnityEngine.Time.deltaTime;
        if (time <= 0)
        {
            DisAbleSprite();
            time = 0;
            isClick = false;
        }
    }
    public void Click()
    {
        if (!isClick)
        {
            time = 4f;
            isClick = true;
            clickSound.Play();
            cardSound.Play();
            onClick?.Invoke();
        }
    }
    private void Update()
    {
        if (isClick)
        {
            FlipCard();
        }
    }
    public void Disable(float time)
    {
        Destroy(gameObject, time);
    }
    public void DisableButton()
    {
        buton.enabled = false;
    }
    public void EnableButton()
    {
        buton.enabled = true;
    }
}
