using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListCardLV : MonoBehaviour
{
    [SerializeField] private List<Card> cards;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Image> images;
    [SerializeField] private GameObject winBar;
    [SerializeField] private List<AudioSource> audios;
    bool allCardNull;
    private void Start()
    {
        CardManager.Instance.onWin.AddListener(CompleteLv);
    }
    public void CompleteLv()
    {
        if (!allCardNull)
        {
            allCardNull = true;
            foreach (Card card in cards)
            {
                if (card != null)
                {
                    allCardNull = false;
                }
            }
            if (allCardNull)
            {
                winBar.SetActive(true);
            }
        }
    }
    public void SwapAllCard()
    {
        foreach (Image image in images)
        {
            if (sprites.Count <= 0) return;
            int index = Random.Range(0, sprites.Count);
            image.sprite = sprites[index];
            sprites.RemoveAt(index);
        }
    }
    
    public void UpdateSound()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < audios.Count; j++)
            {
                if (cards[i].NameCard == audios[j].name && audios[j] != null)
                {
                    cards[i].CardSound = audios[j];
                }
            }
        }
    }
}
