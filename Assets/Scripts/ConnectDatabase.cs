using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectDatabase : MonoBehaviour
{
    public string download = "http://127.0.0.1/getData.php";
    public string update = "http://127.0.0.1/updateData.php";
    public player p;
    public List<player> lp = new List<player>();
    Dictionary<string, string> form = new Dictionary<string, string>();


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("connect");
            DownloadData();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
            UpdateData();
        }
    }
    public void DownloadData()
    {
        StartCoroutine(DB());
    }
    public IEnumerator DB()
    {
        UnityWebRequest www = new UnityWebRequest(download);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        lp = JsonConvert.DeserializeObject<List<player>>(www.downloadHandler.text);

        Debug.Log(lp[0].name);
    }
    public void UpdateData()
    {
        StartCoroutine(UB());
        print("update");
    }
    public IEnumerator UB()
    {
        tojson();
        UnityWebRequest www = UnityWebRequest.Post(update , form);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        //Debug.Log(www.downloadHandler.text);
    }
    public void tojson()
    {
        string j = JsonConvert.SerializeObject(p);
        Debug.Log(j);
        form = new Dictionary<string, string>();
        form.Add("udata", j);
        //form.Add("id", p.id.ToString());
        //form.Add("name", p.name);
        //form.Add("hp", p.hp.ToString());
        //form.Add("atk", p.atk.ToString());

    }
}
[System.Serializable]

public class player
{
    public int id;
    public string name;
    public int hp;
    public int atk;
}
