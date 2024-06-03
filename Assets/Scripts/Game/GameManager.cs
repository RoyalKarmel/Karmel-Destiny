using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Game Manager found!");
            return;
        }

        instance = this;
    }

    #endregion

    public Transform damagePopupPrefab;

    public enum GameState
    {
        MainMenu,
        Playing,
        Battle,
        Shop
    }

    GameState currentState;

    void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                SoundManager.instance.PlayMenuMusic();
                break;
            case GameState.Playing:
                SoundManager.instance.PlayGameMusic();
                break;
            case GameState.Battle:
                SoundManager.instance.PlayBattleMusic();
                break;
            case GameState.Shop:
                SoundManager.instance.PlayShopMusic();
                break;
        }
    }
}
