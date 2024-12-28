using UnityEngine;

namespace Fearth
{
    public class NotifyTest : MonoBehaviour
    {
        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                var popup = NotifyManager.Instance.ShowPopupNotify("Test Message");
                popup.SetHeader("TEST HEADER");
                popup.SetButtonCloseVisible(false);
                popup.SetTextOkay("Okay Baby!");
                popup.SetOkayCallback(() => {Debug.Log("_________OK___________");});
                popup.SetCloseCallback(() => {Debug.Log("_________HIDE___________");});
                popup.SetHideCallback(() => {Debug.Log("_________HIDE___________");});

            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                var popup = NotifyManager.Instance.ShowPopupYesNo("Test Message");
                popup.SetHeader("TEST HEADER");
                popup.SetButtonCloseVisible(false);
                popup.SetTextYes("Yes Baby!");
                popup.SetTextNo("No Baby!");
                popup.SetYesCallback(() => {Debug.Log("_________YES___________");});
                popup.SetNoCallback(() => {Debug.Log("_________NO___________");});
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                NotifyManager.Instance.ShowToast("Test toast...", 3);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                NotifyManager.Instance.ShowProcessingOverlay(3, "Test Message");
            }
        }
    }
}