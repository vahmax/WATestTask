using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Clickable : MonoBehaviour, IPointerDownHandler
{
	public UnityEvent OnClicked;

	public void OnPointerDown(PointerEventData eventData)
	{
		OnClicked?.Invoke();
	}
}
