using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class NotchSpacer : MonoBehaviour
{
    [SerializeField] private bool top;
    [SerializeField] private float min;

    private async void OnEnable()
    {
        await Task.Delay(1);
        var topHeight = (Screen.height - Screen.safeArea.size.y) / 2f;
        var bottomHeight = Screen.safeArea.position.y;
        GetComponent<LayoutElement>().minHeight
            = Mathf.Max(min, top ? topHeight : bottomHeight);
    }
}
