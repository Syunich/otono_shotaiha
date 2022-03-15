using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FirstStage : MonoBehaviour
{
    [SerializeField] CanvasGroup cg_Doors;
    [SerializeField] Transform tr_leftDoor;
    [SerializeField] Transform tr_rightDoor;
    [SerializeField] Transform tr_ghost;
    [SerializeField] Transform tr_hands;
    [SerializeField] Image firsthand;
    [SerializeField] Image HideImage;

    [SerializeField, Header("手型Prefab")] Image handprefab;
    [SerializeField , Header("フェードアウトするまでの手の数")] int handnum;
    [SerializeField, Header("手の出る速度ウェイト")] AnimationCurve curve;

    [SerializeField] Color HandColor1;
    [SerializeField] Color HandColor2;

    private bool CanHide = false;
    private bool NowHidding = false;
    private bool CanStopHandSE;

    private void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(firsthand.DOFade(1, 0))
           .AppendCallback(() => AudioManager.Instance.PlaySE(13))
           .AppendInterval(0.8f)
           .Append(cg_Doors.DOFade(0.3f , 0.5f))
           .Append(tr_ghost.GetComponent<Image>().DOFade(1, 0))
           .Append(tr_leftDoor.DOLocalMoveX(-40, 0.2f))
           .Join(tr_rightDoor.DOLocalMoveX(40, 0.2f))
           .OnComplete(() =>
           {
               StartCoroutine(Shake());
               StartCoroutine(CreateHandInfty());
               AudioManager.Instance.PlaySE(5);
               AudioManager.Instance.PlaySE(10);
           }
           ) 
           .Play();
    }

    private void Update()
    {
        if(CanHide && !NowHidding)
        {
            NowHidding = true;
            var seq = DOTween.Sequence();
            seq.Append(HideImage.DOFade(1, 2.0f))
                .AppendCallback(() => CanStopHandSE = true)
                .AppendInterval(3.5f)
                .OnComplete(() =>
                {
                    GameManager.Instance.CanGoNext = true;
                    Destroy(this.gameObject);
                }
                ).Play();
        }
    }

    private IEnumerator Shake()
    {
        while (true)
        {
            tr_ghost.localPosition = new Vector3(0, 10, 0);
            yield return new WaitForSeconds(0.1f);
            tr_ghost.localPosition = new Vector3(0, -10, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Createhand()
    {
        var RandPos = new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0);
        var RotateZ = Random.Range(0, 360);
        var Scale = Random.Range(1f, 3f);
        var hand = Instantiate(handprefab);
        hand.transform.SetParent(tr_hands);
        hand.transform.localPosition = RandPos;
        hand.transform.rotation = Quaternion.Euler(0, 0, RotateZ);
        hand.transform.localScale = new Vector3(Scale, Scale, 1);
        hand.GetComponent<Image>().color = Color.Lerp(HandColor1, HandColor2, Random.value);
    }

    private IEnumerator CreateHandInfty()
    {
        int count = 0;
        var StartTime = Time.time;
        while(true)
        {
            Createhand();
            count++;
            Debug.Log(Time.time - StartTime);
            yield return new WaitForSeconds(curve.Evaluate(Time.time - StartTime) / 10);
            if(count > handnum)
            {
                CanHide = true;
            }
            if(!CanStopHandSE)
            {
                AudioManager.Instance.PlaySE(Random.Range(16, 18));
            }
        }
    }
}
