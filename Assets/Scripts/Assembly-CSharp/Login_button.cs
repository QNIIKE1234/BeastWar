using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Bridge script: Delegates to modern LoginManager while maintaining backward compatibility with legacy scenes.
/// </summary>
public class Login_button : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public Text user_input_name;

	public void OnPointerDown(PointerEventData eventData)
	{
		TriggerLogin();
	}

	public void TriggerLogin()
	{
		if (LoginManager.Instance != null)
		{
			LoginManager.Instance.Login();
			return;
		}

		// Fallback if LoginManager is not present
		string name = (user_input_name != null && !string.IsNullOrEmpty(user_input_name.text)) 
			? user_input_name.text.ToString() 
			: PlayerPrefs.GetString("fam_name", "12TailsOffline");
		PlayerPrefs.SetString("fam_name", name);
		Initiate.Fade("L10_LobbyGuild", Color.white, 0.4f);
	}
}
