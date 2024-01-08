using UnityEngine;
using Button = UnityEngine.UI.Button;

public class OkButtonController : MonoBehaviour
{
    void Start()
    {
        var button = GetComponent<Button>();

        var clickEvent = new Button.ButtonClickedEvent();
        clickEvent.AddListener(() => GetComponentInParent<DialogController>().OnDismiss());

        button.onClick = clickEvent;
    }

    // Update is called once per frame
    void Update()
    {
    }
}