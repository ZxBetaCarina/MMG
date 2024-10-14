using UnityEngine;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject image;

    private void OnEnable()
    {
        RotateImage();
    }

    private void OnDisable()
    {
        DOTween.Kill(image.transform);
    }

    private void RotateImage()
    {
        image.transform.rotation = Quaternion.identity;
        image.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}