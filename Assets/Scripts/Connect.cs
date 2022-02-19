using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class Connect : MonoBehaviour
{
    public string uri;
    public LoginData p;

    public void Send()
    {
        StartCoroutine(SendData());
    }
    IEnumerator SendData()
    {
        string data = JsonConvert.SerializeObject(p);
        WWWForm form = new WWWForm();
        form.AddField("account", p.account);
        form.AddField("password", p.password);

        UnityWebRequest www = UnityWebRequest.Post(uri ,form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("connect complete!");
        }

        Debug.Log(www.downloadHandler.text);
    }
}
[System.Serializable]
public class LoginData
{
    public string account;
    public string password;
}
