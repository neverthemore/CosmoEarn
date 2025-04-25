using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    [SerializeField] float FadeTime = 2f;

    [SerializeField] GameObject ComicsPanel;
    [SerializeField] Image ComicsBackground;
    [SerializeField] float ComicsTime = 2f;

    [SerializeField] Image[] ComicsImages;
    [SerializeField] float ComicsPageTime = 0.2f;

    public void StartGame()
    {
        Debug.Log("Начали");
        StartCoroutine(StartAnim());
    }

    private IEnumerator StartAnim()
    {
        Color color = FadeImage.color;
        FadeImage.gameObject.SetActive(true);

        while (color.a < 1)
        {
            color.a += Time.deltaTime / FadeTime;
            FadeImage.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        ComicsPanel.gameObject.SetActive(true);
        color = ComicsBackground.color;
        while (color.a < 1)
        {
            color.a += Time.deltaTime / ComicsTime;
            ComicsBackground.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        foreach (var image in ComicsImages)
        {
            image.gameObject.SetActive(true);
            color = image.color;
            while (color.a < 1)
            {
                color.a += Time.deltaTime / ComicsPageTime;
                image.color = color;
                yield return null;
            }
            yield return new WaitForSeconds(3f);
        }        
    }
}
