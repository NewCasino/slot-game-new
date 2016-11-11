using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class Reel : MonoBehaviour {

    public int ID;
    public GameObject obj;
    public List<SymbolInstance> listSymbolObj = new List<SymbolInstance>();
    public List<SymbolInstance> listSymbolObjForAni = new List<SymbolInstance>();

    /// <summary>
    /// 旋转卷轴
    /// </summary>
    public void StartSpin(float delayTime, float speed)
    {
        //Vector2 pos;
        //SymbolController symbolController = UIMgr.Instance.GetView(EViewID.Symbol) as SymbolController;
        //symbolController.
        //SymbolView(Clone)
        //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
        //    string.Format((listSymbolObj[0].SymbolObj.GetComponent<RectTransform>().localPosition,
        //    canvas.camera, out pos))
        //{
        //    Debug.Log(string.Format((listSymbolObj[0].SymbolObj.GetComponent<RectTransform>().localPosition) + "       " + ID.ToString()));
        //}
        Debug.Log(string.Format((listSymbolObj[0].SymbolObj.GetComponent<RectTransform>().localPosition) + "       " + ID.ToString()));
        Debug.Log(string.Format((listSymbolObj[1].SymbolObj.GetComponent<RectTransform>().anchoredPosition) + "       " + ID.ToString()));
        Debug.Log(string.Format((listSymbolObj[2].SymbolObj.GetComponent<RectTransform>().anchoredPosition) + "       " + ID.ToString()));
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);

        GridLayoutGroup gridLayoutGroup = this.obj.GetComponent<GridLayoutGroup>();
        if (null != gridLayoutGroup)
        {
            gridLayoutGroup.enabled = false;
        }
        this.obj.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
        Tweener dealyDotween = this.obj.transform.DOLocalMoveY(0, delayTime).SetRelative().SetLoops(1, LoopType.Incremental);
        float height = this.listSymbolObj[0].SymbolObj.GetComponent<RectTransform>().rect.height;
        int count = listSymbolObjForAni.Count;
        dealyDotween.OnComplete(new TweenCallback(() =>
        {
            Tweener SlowMoveDotween = this.obj.transform.DOLocalMoveY(-height* listSymbolObj.Count, height * listSymbolObj.Count * speed*1.8f)
                                          .SetLoops(1, LoopType.Incremental)
                                          .SetEase(Ease.InQuad);

            SlowMoveDotween.OnComplete(new TweenCallback(() =>
            {
                Tweener fastMoveDotween = this.obj.transform.DOLocalMoveY(-height* count, height * count * speed)
                                            .SetLoops(1, LoopType.Incremental)
                                            .SetEase(Ease.Linear).SetId("moveDotween");

                fastMoveDotween.OnComplete(new TweenCallback(() =>
                {
                    if (null != gridLayoutGroup)
                    {
                        gridLayoutGroup.enabled = true;
                    }
                    SymbolController symbolController = UIMgr.Instance.GetView(EViewID.Symbol) as SymbolController;
                    if (this.ID == symbolController.lastReelID)
                    {
                        symbolController.EndSpinReel();
                    }

                }));
            }));
        }));
    }
}
