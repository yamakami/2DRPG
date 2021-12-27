using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using Cysharp.Threading.Tasks;
using  System;

public class MyTest : MonoBehaviour
{    
    static string hello;

    async UniTaskVoid Start()
    // void  Start()
    {
        // var  cts = new CancellationTokenSource();
        // var token = cts.Token;  

        // var tween = Create();
        // // tween.Play();
        // await tween.Play().ToUniTask(cancellationToken: token);
        // Debug.Log("end");
    }

    static void StringOut()
    {
        Debug.LogFormat("yahoo {0}", hello);
    }

    Tween Create()
    {
        hello = "ほげー";
        var a = "aaaaaa";

        var sequence = DOTween.Sequence();
        sequence.SetDelay(3f)
        // sequence.OnStart(() => {
        //     Debug.Log("hello");
        // });
        .AppendCallback(()=> {
            // StringOut();
        })

        .AppendInterval(2f)

        .AppendCallback(() => {
           Debug.LogFormat("hello 2");
        //    Debug.LogFormat("hello 2 {0}", a);
        });
        return sequence;
    }
}
