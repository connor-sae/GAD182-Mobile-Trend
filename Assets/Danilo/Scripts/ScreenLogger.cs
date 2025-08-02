using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLogger : MonoBehaviour
{
    private static ScreenLogger I;

    [SerializeField] private GameObject logPrefab;
    [SerializeField] private int maxLogLength;
    [SerializeField] private Transform logPos;

    private LogObject[] logs;

    private void Awake()
    {
        if (I == null)
            I = this;
        else
        {
            Destroy(I);
            Debug.LogWarning("Duplicate Screenlogger was destroyed");
        }

        logs = new LogObject[maxLogLength];
       
    }

    public static void Log(string text)
    {
        I.addLog(text);
    }

    private void addLog(string text)
    {
        LogObject newLog = Instantiate(logPrefab, logPos).GetComponent<LogObject>();
        newLog.text = text;

        if(newLog == null)
        {
            Debug.LogError("The Assigned LogPrefab does not have a LogObject Script!");
            return;
        }

        logs[^1]?.Remove();

        for(int i = logs.Length - 1; i > 0; i--)
        {

            logs[i] = logs[i - 1];
            logs[i]?.Push();

        }

        logs[0] = newLog;
        logs[0].Push();
        
    }
}
