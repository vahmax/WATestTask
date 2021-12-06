using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Clickable : MonoBehaviour
{
	public UnityEvent OnClicked;

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		OnClicked?.Invoke();
	}
}
