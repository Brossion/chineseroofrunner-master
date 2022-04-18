using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerManager.isGameStarted)
        {
            transform.DOShakePosition(5, new Vector3(0.4f, 0, 0), 10).SetLoops(-1, LoopType.Yoyo);
            return;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
    }
}
