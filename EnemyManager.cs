using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] FailedPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.StopGame();
            GameManager.Instance.AddMaxScore();
            Instantiate(FailedPrefab[GameManager.Instance.NowStageNum]);
        }
    }
}
