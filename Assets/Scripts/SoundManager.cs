using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instan�e = null;
    public AudioSource efxSource1;
    public AudioSource efxSource2;

    public AudioClip gameOverSound;
    public AudioClip eatFoodSound;
    public AudioClip moveSound;


    void Awake()
    {
        //����������, ���������� �� ��� ��������� SoundManager
        if (instan�e == null)
            // ���� ��� ������ ������ ��������� ��������
            instan�e = this;
        else if (instan�e != this) // ���� ����������
            Destroy(gameObject); // �������, ����������� ������� ��������, ����� ��� ��������� ������ ����� ���� ������ ����
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        float randomPitch = Random.Range(0.95f, 1.05f);
        source.pitch = randomPitch;
        source.clip = clip;
        source.Play();
        
    }
}
