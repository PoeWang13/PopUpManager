using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopUpTester : MonoBehaviour
{
    private void Start()
    {
        PopUp_Manager.Instance.SetTitle("SetTitle1").SetMessage("SetMessage1")
            .SetPozitifAction(pozitif).SetNegatifAction(negatif)
            .SetInputAction(input).PopUpPanelGoster();
        PopUp_Manager.Instance.SetTitle("SetTitle2").SetMessage("SetMessage2")
            .SetPozitifAction(pozitif).SetNegatifAction(negatif)
            .SetInputAction(input).PopUpPanelGoster();
        PopUp_Manager.Instance.SetTitle("SetTitle3").SetMessage("SetMessage3")
            .SetPozitifAction(pozitif).SetNegatifAction(negatif)
            .SetInputAction(input).PopUpPanelGoster();
        PopUp_Manager.Instance.SetTitle("SetTitle4").SetMessage("SetMessage3")
            .SetPozitifAction(pozitif).SetNegatifAction(negatif)
            .SetInputAction(input).PopUpPanelGoster();
    }
    private void negatif()
    {
        Debug.Log("negatif");
    }
    private void pozitif()
    {
        Debug.Log("pozitif");
    }
    private void input(int s)
    {
        Debug.Log("input--" + (s == 5));
    }
}