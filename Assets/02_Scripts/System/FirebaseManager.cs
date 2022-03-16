using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using GooglePlayGames;
using Firebase.Auth;

public class FirebaseManager : MonoBehaviour
{
    static FirebaseManager unique;
    public static FirebaseManager instance { get { return unique; } }


    private FirebaseAuth m_auth;
    public string m_sFireBaseId = string.Empty;

    IEnumerator TryFirebaseLogin()
    {
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        m_auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            m_sFireBaseId = newUser.UserId;
        });
    }

    public void FirebaseLoginStart()
    {
        StartCoroutine(TryFirebaseLogin());
    }

    public void FirebaseLogout()
    {
        m_auth.SignOut();
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
            Debug.LogWarning("파이어베이스 매니저 복수 존재");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_auth = FirebaseAuth.DefaultInstance;
    }
}
