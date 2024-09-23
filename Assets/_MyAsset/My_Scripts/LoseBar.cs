using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseBar : MonoBehaviour
{
    [SerializeField] private LoseBar lose;
    public void PlayAgain()
    {
        lose.enabled = false;
    }
    private void Dislay()
    {
        if (lose.enabled == false)
        {
            CardManager.Instance.Index = 0;
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        Invoke(nameof(Dislay), 2f);
    }
}
