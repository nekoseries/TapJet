using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseLoader : MonoBehaviour
{
    public static bool FirebaseStatus;
    void Awake()
    {
        FirebaseStatus = false;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                FirebaseStatus = true;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
}
