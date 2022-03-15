using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAppier
{
    private readonly string[] _messages;
    private int _counter;
    public bool IsEnd { get; private set; }
    public MessageAppier(string[] messages)
    {
        _counter = 0;
        _messages = messages;
        if(_messages.Length == 0)
        {
            IsEnd = true;
        }
        else
        {
            IsEnd = false;
        }
    }

    /// <summary>
    /// 次の文字列を読み込む
    /// </summary>
    /// <returns></returns>
    public string Next()
    {
        if (IsEnd)
        {
            Debug.Log("最大長に達しました");
            return null;
        }
        var str = _messages[_counter];

        if (++_counter == _messages.Length)
        {
            IsEnd = true;
        }

        return str;
    }
}
