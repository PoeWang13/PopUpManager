using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class PopUp
{
    public string title = "My Title";
    public string message = "My Message";
    public float fadeInDuration = 1.0f;
    public Color pozitifButtonColor = Color.white;
    public string pozitifButtonTextString = "Yes";
    public UnityAction pozitifUnityAction = null;
    public Color negatifButtonColor = Color.white;
    public string negatifButtonTextString = "No";
    public UnityAction negatifUnityAction = null;
    public UnityAction<int> inputUnityAction = null;
}
public class PopUp_Manager : MonoSingletion<PopUp_Manager>
{
    [Header("Genel Script Atamaları")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform butonsTransform;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject genelPopUpPanel;

    private bool isActive = false;
    private PopUp myPopUp = new PopUp();
    private PopUp myUsingPopUp;
    private Queue<PopUp> popUps = new Queue<PopUp>();

    [Header("Pozitif Button Atamaları")]
    [SerializeField] private Button pozitifButton;
    [SerializeField] private Image pozitifButtonImage;
    [SerializeField] private TextMeshProUGUI pozitifButtonText;

    [Header("Negatif Button Atamaları")]
    [SerializeField] private Button negatifButton;
    [SerializeField] private Image negatifButtonImage;
    [SerializeField] private TextMeshProUGUI negatifButtonText;

    [Header("Slot Atamaları")]
    [SerializeField] private GameObject slotGameObject;
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI slotAmountText;

    [Header("Input Atamaları")]
    [SerializeField] private GameObject inputGameObject;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private bool inputActive = false;

    private IEnumerator FadeTimer;
    private void Start()
    {
        FadeTimer = FadeTime(myPopUp.fadeInDuration);
        pozitifButton.onClick.AddListener(PopUpPozitifAnswer);
        negatifButton.onClick.AddListener(PopUpNegatifAnswer);
    }
    // PopUp Panel Ayarlandıktan sonra paneli göstermek için çağrılır. Bu yüzden en son yazılması gerekir.
    public PopUp_Manager PopUpPanelGoster()
    {
        popUps.Enqueue(myPopUp);
        // Temizle Herşeyi
        myPopUp = new PopUp();
        if (!isActive)
        {
            SiradakiPopUpGoster();
        }
        return Instance;
    }
    public PopUp_Manager SetInputAction(UnityAction<int> inputAction)
    {
        // butonları -100 e taşı
        butonsTransform.anchoredPosition = new Vector2(butonsTransform.anchoredPosition.x, -100);
        inputActive = true;
        inputGameObject.SetActive(true);
        myPopUp.inputUnityAction = inputAction;
        return Instance;
    }
    public PopUp_Manager SetTitle(string title)
    {
        myPopUp.title = title;
        return Instance;
    }
    public PopUp_Manager SetMessage(string message)
    {
        myPopUp.message = message;
        return Instance;
    }
    public PopUp_Manager SetFadeInDuration(float duration)
    {
        myPopUp.fadeInDuration = duration;
        return Instance;
    }
    private IEnumerator FadeTime(float duration)
    {
        float startingTime = Time.time;
        float alphaTime = 0.0f;
        while (alphaTime < 1)
        {
            alphaTime = Mathf.Lerp(0.0f, 1.0f, (Time.time - startingTime) / duration);
            canvasGroup.alpha = alphaTime;
            yield return null;
        }
    }
    public PopUp_Manager ItemSlot(Sprite item, string slotAmount)
    {
        slotGameObject.SetActive(true);
        butonsTransform.anchoredPosition = new Vector2(butonsTransform.anchoredPosition.x, -100);
        slotImage.sprite = item;
        slotAmountText.text = slotAmount;
        return Instance;
    }
    public PopUp_Manager InputField(string inputAmount)
    {
        inputGameObject.SetActive(true);
        inputActive = true;
        inputField.text = inputAmount;
        return Instance;
    }
    public PopUp_Manager SetPozitifButtonText(string pozitifText)
    {
        myPopUp.pozitifButtonTextString = pozitifText;
        return Instance;
    }
    public PopUp_Manager SetNegatifButtonText(string negatifText)
    {
        myPopUp.negatifButtonTextString = negatifText;
        return Instance;
    }
    public PopUp_Manager SetPozitifButtonActiver(bool isActive)
    {
        pozitifButton.gameObject.SetActive(isActive);
        return Instance;
    }
    public PopUp_Manager SetNegatifButtonActiver(bool isActive)
    {
        negatifButton.gameObject.SetActive(isActive);
        return Instance;
    }
    public PopUp_Manager SetPozitifButtonColor(Color pozitifColor)
    {
        myPopUp.pozitifButtonColor = pozitifColor;
        return Instance;
    }
    public PopUp_Manager SetNegatifButtonColor(Color negatifColor)
    {
        myPopUp.negatifButtonColor = negatifColor;
        return Instance;
    }
    public PopUp_Manager SetPozitifAction(UnityAction pozitifAction)
    {
        myPopUp.pozitifUnityAction = pozitifAction;
        return Instance;
    }
    public PopUp_Manager SetNegatifAction(UnityAction negatifAction)
    {
        myPopUp.negatifUnityAction = negatifAction;
        return Instance;
    }
    private void PopUpPozitifAnswer()
    {
        if (inputActive)
        {
            if (int.TryParse(inputField.text, out int deger))
            {
                myPopUp.inputUnityAction?.Invoke(deger);
            }
            else
            {
                myPopUp.inputUnityAction?.Invoke(-555555);
            }
        }
        else
        {
            myPopUp.pozitifUnityAction?.Invoke();
        }

        PopUpPanelSakla();
    }
    private void PopUpNegatifAnswer()
    {
        myPopUp.negatifUnityAction?.Invoke();

        PopUpPanelSakla();
    }
    private void SiradakiPopUpGoster()
    {
        myUsingPopUp = popUps.Dequeue();

        titleText.text = myUsingPopUp.title;
        messageText.text = myUsingPopUp.message;
        pozitifButtonImage.color = myUsingPopUp.pozitifButtonColor;
        negatifButtonImage.color = myUsingPopUp.negatifButtonColor;
        pozitifButtonText.text = myUsingPopUp.pozitifButtonTextString;
        negatifButtonText.text = myUsingPopUp.negatifButtonTextString;

        genelPopUpPanel.SetActive(true);
        isActive = true;
        StartCoroutine(FadeTime(myUsingPopUp.fadeInDuration));
    }
    private void PopUpPanelSakla()
    {
        isActive = false;
        StopCoroutine(FadeTimer);
        slotGameObject.SetActive(false);
        inputGameObject.SetActive(false);
        inputActive = false;
        // butonları -35 e taşı
        butonsTransform.anchoredPosition = new Vector2(butonsTransform.anchoredPosition.x, -50);
        genelPopUpPanel.SetActive(false);

        if (popUps.Count > 0)
        {
            SiradakiPopUpGoster();
        }
    }
}