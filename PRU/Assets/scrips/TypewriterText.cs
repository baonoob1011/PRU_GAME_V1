using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterText : MonoBehaviour
{
    public TextMeshProUGUI textTMP;

    [TextArea(3, 6)]
    public string[] pages;   // mỗi phần là 1 đoạn

    public float typingSpeed = 0.05f;
    public float delayBetweenPages = 1f;

    public bool IsFinished { get; private set; }

    void OnEnable()
    {
        StartCoroutine(PlayPages());
    }

    IEnumerator PlayPages()
    {
        IsFinished = false;

        for (int i = 0; i < pages.Length; i++)
        {
            textTMP.text = "";

            foreach (char c in pages[i])
            {
                textTMP.text += c;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }

            // chạy xong 1 đoạn → đợi rồi chuyển đoạn
            yield return new WaitForSecondsRealtime(delayBetweenPages);
        }

        IsFinished = true;
    }
}
