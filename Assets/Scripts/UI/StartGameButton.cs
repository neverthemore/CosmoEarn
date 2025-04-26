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

    [SerializeField] GameObject AngarPanel;

    [SerializeField] Image FadeAngarImage;

    private void Start()
    {

        Color transColor = new Color(1, 1, 1, 0);
        
        ComicsPanel.gameObject.SetActive(false);
        ComicsBackground.color = transColor;

        foreach (var image in ComicsImages)
        {
            image.color = transColor;
            image.gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        AngarPanel.gameObject.SetActive(false);

        transColor = ComicsBackground.color;
        transColor.a = 0;
        ComicsBackground.color = transColor;

        transColor = new Color(0, 0, 0, 0);
        FadeImage.color = transColor;
        FadeAngarImage.color = transColor;
        FadeImage.gameObject.SetActive(false);
    }

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
        
        AngarPanel.gameObject.SetActive(true);
        FadeAngarImage.gameObject.SetActive(true);
        color = FadeAngarImage.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime / FadeTime;
            FadeImage.color = color;
            yield return null;
        }
        FadeAngarImage.gameObject.SetActive(false);

    }
}
