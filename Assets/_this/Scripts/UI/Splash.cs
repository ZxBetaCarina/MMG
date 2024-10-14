using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float fadeDuration;
    [SerializeField] float splashDuration;

    private IEnumerator Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        yield return new WaitForSeconds(splashDuration);
        AppearanceManager.Singleton.FadeIn(image.gameObject, splashDuration, AnimationDirection.Down,
            () => StartCoroutine(SplashOut()));
    }

    IEnumerator SplashOut()
    {
        yield return new WaitForSeconds(fadeDuration);
        AppearanceManager.Singleton.FadeOut(image.gameObject, splashDuration, AnimationDirection.Up, AfterThis);
    }

    private void AfterThis()
    {
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }
}