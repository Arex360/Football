using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ResultSystem : MonoBehaviour
{
    [SerializeField] private UnityEvent onCompleteResult;
    public void Result()
    {
        onCompleteResult.Invoke();
        this.gameObject.SetActive(false);
    }
}
