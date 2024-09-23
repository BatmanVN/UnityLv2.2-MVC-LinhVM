using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBar : MonoBehaviour
{
    [SerializeField] private List<ListCardLV> cardLevels;
    [SerializeField] private GameObject win;
    [SerializeField] private int lv;
    public void NextLv()
    {
        if (lv > 3 || lv < 0) return;
        lv += 1;
        Dislay();
        win.SetActive(false);
    }
    private void Dislay()
    {
        for (int i = 0; i < cardLevels.Count; i++)
        {
            CardManager.Instance.Index = 0;
            cardLevels[i].gameObject.SetActive(i == lv);
        }
    }
    private void Update()
    {

    }
}
