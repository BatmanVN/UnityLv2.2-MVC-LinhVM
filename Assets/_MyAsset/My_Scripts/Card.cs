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
    [SerializeField] private GameObject[] typeCard;
    [SerializeField] private Image image;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private AudioSource cardSound;
    [SerializeField] private Button buton;
    [SerializeField] private float time;
    [SerializeField] private bool isClick;
    [SerializeField] private UnityEvent onClick;
    [SerializeField] private UnityEvent onSwap;
    [SerializeField] private UnityEvent onAddSound;
    
    public string NameCard { get => nameCard; }
    public AudioSource CardSound { get => cardSound; set => cardSound = value; }

    private void Awake()
    {
         
    }
    private void Start()
    {
        ShowCard();
    }
    private void ShowCard()
    {
        isClick = true;
        EnableFlip();
        Invoke(nameof(DisableAnswer), 2f);
        Invoke(nameof(Swap), 3f);
    }
    private void IsclickFalse()
    {
        isClick = false;
    }
    private void Swap()
    {
        cardAnim.SetTrigger("swap");
        onSwap?.Invoke();
        ChangeSprite();
        onAddSound?.Invoke();
    }

    private IEnumerator Delay()
    {
        float currentRotation = gameObject.transform.eulerAngles.z;
        yield return new WaitForSeconds(0.3f);
        currentRotation += 20;
        yield return new WaitForSeconds(0.4f);
        currentRotation -= 20;
        yield return new WaitForSeconds(0.5f);
        currentRotation -= 20;
        yield return new WaitForSeconds(0.6f);
        currentRotation += 20;
    }
    
    public void ShakeCard()
    {
        StartCoroutine(Delay());
    }
    public void EnableFlip()
    {
        int cardAnswer = 1;
        typeCard[cardAnswer].SetActive(true);
    }
    public void DisableFlip()
    {
        int cardAnswer = 1;
        typeCard[cardAnswer].SetActive(false);
    }
    private void DisableAnswer()
    {
        cardAnim.SetTrigger("fail");
    }
    private void FlipCard()
    {
        cardAnim.SetBool(flipParaname, true);
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
        time -= Time.deltaTime;
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
            CardSound.Play();
            onClick?.Invoke();
        }
    }
    private void Update()
    {
        if(isClick)
            FlipCard();
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
