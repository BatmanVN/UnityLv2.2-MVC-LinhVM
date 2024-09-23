using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private List<Card> cards;
    [SerializeField] private List<ListCardLV> listCards;
    [SerializeField] private GameObject hand;
    [SerializeField] private float time;
    private void CheckHand()
    {
        foreach (ListCardLV list in listCards)
        {
            if (list.enabled)
            {
                
            }
        }
    }
    private void Update()
    {
        time -= Time.deltaTime;
        CheckHand();
    }
}
