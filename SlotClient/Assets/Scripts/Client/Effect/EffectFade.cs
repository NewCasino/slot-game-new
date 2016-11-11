using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

[AddComponentMenu("Slot/Effects/Fade")]
public class EffectFade : EffectBase
{
	void Start ()
	{
		RectTransform trans = this.GetComponent<RectTransform>();
		Text text = this.GetComponent<Text>();

		DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
		Sequence dtSequence = DOTween.Sequence();
		dtSequence.Append(trans.DOLocalMove(new Vector3(0, 100, 0), 1).SetRelative());
		//dtSequence.Append(text.material.DOFade(0, 1).SetRelative());
		dtSequence.OnComplete(OnTweenComplete);
	}

	private void OnTweenComplete()
	{
		Destroy(gameObject);
	}
}
