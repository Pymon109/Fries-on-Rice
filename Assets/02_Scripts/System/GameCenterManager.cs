using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameCenterManager : MonoBehaviour
{
    static GameCenterManager unique;
    public static GameCenterManager instance { get { return unique; } }

    public string email;
    [SerializeField] Text m_txtEmail;

    ///////////////////////////////////////////////////////////////////////////////로그인 관련 함수
    public static bool IsAuthenticated()
    {
        return Social.localUser.authenticated;
    }

    public void Login()
    {
        Debug.Log("GameCenter Login");
        if(!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(success =>
            {
                if(success)
                {
                    Debug.Log("google game service Success");
                    email = "email : " + Social.localUser.id + "\n" + Social.localUser.userName;
                    //firebase login 시도
                    FirebaseManager.instance.FirebaseLoginStart();
                }
                else
                {
                    Debug.Log("google game service Fail");
                    email = "google game service Fail";
                }
            });
        }
        m_txtEmail.text = email;
    }

    public void TryGoogleLogout()
    {
        if(Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            FirebaseManager.instance.FirebaseLogout();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////리더보드 관련 함수

    public void ReportScore(IngameManager.E_GAMEMODE mode, int record)
    {
        string sLeaderBoardID = "";
        switch (mode)
        {
            case IngameManager.E_GAMEMODE.NORMAL:
                sLeaderBoardID = GPGSIds.leaderboard_best_record_on_normal_difficulty;
                break;
            case IngameManager.E_GAMEMODE.HARD:
                sLeaderBoardID = GPGSIds.leaderboard_best_record_on_hard_difficulty;
                break;
        }
        Social.ReportScore(record, sLeaderBoardID, success =>
        {
            if (success)
            {
                Debug.Log("record score success :" + sLeaderBoardID);
            }
            else
            {
                Debug.Log("record score fail :" + sLeaderBoardID);
            }
        });

    }

    public void ShowLeaderBoeard()
    {
        Social.ShowLeaderboardUI();
    }

    private void Awake()
    {
        if (unique == null)
        {
            unique = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("게임 센터 매니저 복수 존재");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());

        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().RequestIdToken().Build();
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Login();
    }

}
