using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalkthroughCarouselAnimated : MonoBehaviour
{
    [System.Serializable]
    public class Slide
    {
        public Sprite image;
        [TextArea] public string caption;
    }

    [Header("Slides")]
    public Slide[] slides;

    [Header("Slide Panels (A and B)")]
    public RectTransform slideA;
    public RectTransform slideB;

    public Image imageA;
    public TMP_Text captionA;
    public CanvasGroup groupA;

    public Image imageB;
    public TMP_Text captionB;
    public CanvasGroup groupB;

    [Header("Animation")]
    public float duration = 0.25f;
    public float slideDistance = 1700f; 

    int index = 0;
    bool animating = false;
    bool aIsCurrent = true;

    void Start()
    {
        if (slides == null || slides.Length == 0) return;

        SetContent(imageA, captionA, slides[index]);
        groupA.alpha = 1f;
        groupB.alpha = 0f;

        slideA.anchoredPosition = Vector2.zero;
        slideB.anchoredPosition = Vector2.zero;
    }

    public void Next() => Change(+1);
    public void Prev() => Change(-1);

    void Change(int dir)
    {
        if (animating || slides == null || slides.Length == 0) return;

        int newIndex = (index + dir + slides.Length) % slides.Length;
        StartCoroutine(AnimateTo(newIndex, dir));
    }

    IEnumerator AnimateTo(int newIndex, int dir)
    {
        animating = true;

        // Current (from) and Incoming (to)
        RectTransform fromRT = aIsCurrent ? slideA : slideB;
        RectTransform toRT   = aIsCurrent ? slideB : slideA;

        Image fromImg = aIsCurrent ? imageA : imageB;
        TMP_Text fromCap = aIsCurrent ? captionA : captionB;
        CanvasGroup fromGroup = aIsCurrent ? groupA : groupB;

        Image toImg = aIsCurrent ? imageB : imageA;
        TMP_Text toCap = aIsCurrent ? captionB : captionA;
        CanvasGroup toGroup = aIsCurrent ? groupB : groupA;

        // Set incoming slide content
        SetContent(toImg, toCap, slides[newIndex]);

        // Place incoming offscreen (dir: +1 means move left, new comes from right)
        float startX = dir > 0 ? slideDistance : -slideDistance;
        float endX = -startX;

        toRT.anchoredPosition = new Vector2(startX, 0f);
        toGroup.alpha = 0f;

        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // UI feels better not tied to timescale
            float p = Mathf.Clamp01(t / duration);

            // Smooth easing
            float eased = EaseInOut(p);

            // Move
            fromRT.anchoredPosition = new Vector2(Mathf.Lerp(0f, endX, eased), 0f);
            toRT.anchoredPosition   = new Vector2(Mathf.Lerp(startX, 0f, eased), 0f);

            // Fade
            fromGroup.alpha = Mathf.Lerp(1f, 0f, eased);
            toGroup.alpha   = Mathf.Lerp(0f, 1f, eased);

            yield return null;
        }

        // Snap to final state
        fromRT.anchoredPosition = Vector2.zero;
        toRT.anchoredPosition = Vector2.zero;

        fromGroup.alpha = 0f;
        toGroup.alpha = 1f;

        index = newIndex;
        aIsCurrent = !aIsCurrent;
        animating = false;
    }

    void SetContent(Image img, TMP_Text cap, Slide s)
    {
        img.sprite = s.image;
        cap.text = s.caption;
    }

    float EaseInOut(float x)
    {
        // Smoothstep
        return x * x * (3f - 2f * x);
    }
}
