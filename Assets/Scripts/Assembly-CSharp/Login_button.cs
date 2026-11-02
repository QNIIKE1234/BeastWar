using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Login_button : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public Text user_input_name;

	private void Start()
	{
		if (user_input_name != null && string.IsNullOrEmpty(user_input_name.text))
		{
			user_input_name.text = PlayerPrefs.GetString("fam_name", "12TailsOffline");
		}
	}

	private void Update()
	{
		// Press Enter, Space, or Keypad Enter to login immediately
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
		{
			TriggerLogin();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		TriggerLogin();
	}

	public void TriggerLogin()
	{
		string name = (user_input_name != null && !string.IsNullOrEmpty(user_input_name.text)) 
			? user_input_name.text.ToString() 
			: PlayerPrefs.GetString("fam_name", "12TailsOffline");
		PlayerPrefs.SetString("fam_name", name);
		Initiate.Fade("L10_LobbyGuild", Color.white, 0.4f);
	}
}
