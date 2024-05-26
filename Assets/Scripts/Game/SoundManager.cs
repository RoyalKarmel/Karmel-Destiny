using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one isntance SoundManager found!");
            return;
        }

        instance = this;
    }
    #endregion

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip battleMusic;
    public AudioClip shopMusic;

    [Header("SFX Clips")]
    public AudioClip itemPurchase;
    public AudioClip itemUse;
    public AudioClip itemEquip;

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Methods to play specific music
    public void PlayMenuMusic() => PlayMusic(menuMusic);

    public void PlayGameMusic() => PlayMusic(gameMusic);

    public void PlayBattleMusic() => PlayMusic(battleMusic);

    public void PlayShopMusic() => PlayMusic(shopMusic);

    // Methods to play specific sound effects
    public void PlayItemPurchase() => PlaySFX(itemPurchase);

    public void PlayItemUse() => PlaySFX(itemUse);

    public void PlayItemEquip() => PlaySFX(itemEquip);
}
