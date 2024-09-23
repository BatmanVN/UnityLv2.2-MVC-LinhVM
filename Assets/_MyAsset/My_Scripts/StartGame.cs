using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private GameObject bar;
    [SerializeField] private UnityEvent onStart;
    public void GameStart()
    {
        music.Play();
        bar.SetActive(false);
        Time.timeScale = 1;
        onStart?.Invoke();
    }
}
