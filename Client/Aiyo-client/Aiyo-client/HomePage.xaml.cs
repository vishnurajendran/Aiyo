using Aiyo_Client.Controls;
using Aiyo_Core.RemoteEvents;
using Newtonsoft.Json;
using Aiyo_Client.DataContainers;

namespace Aiyo_Client;

public partial class HomePage : ContentPage, IRemoteEventHandler
{
    public int SupportedEventId => Constants.AyioClientConstants.MESSAGE_EVENT;

    public HomePage()
	{
		InitializeComponent();
        SetupUI();
		RemoteEventBus.RegisterEventHandler(this);
	}

	private void SetupUI()
	{
		sendButton.Clicked += OnSendClicked;
	}

	private void OnSendClicked(object sender, EventArgs eventArgs)
	{
		var text = messageInputField.Text;
		if (string.IsNullOrEmpty(text))
			return;

		messageInputField.Text = string.Empty;
		var dataContainer = GetMessageDataContainer(text);
        AddToMessageHistory(true,dataContainer);
		RemoteEventBus.SubmitEvent(this, JsonConvert.SerializeObject(dataContainer));
        FakeReply();
    }

	private MessageDataContainer GetMessageDataContainer(string message)
	{
        long unixTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        return new MessageDataContainer()
		{
			SendTime = unixTime,
			Message = message
		};
	}

    private async void FakeReply()
	{
		var rand = new Random();
		await Task.Delay(rand.Next(1000,5000));
		AddToMessageHistory(false,GetMessageDataContainer("Some Gibberish Karthik says"));
	}

	private void AddToMessageHistory(bool isMine, MessageDataContainer dataContainer)
	{
		var bubble = new MessageBubble(isMine);
		bubble.SetText(dataContainer.Message);
		bubble.SetTime(dataContainer.SendTime);
		messagesArea.Add(bubble);
	}

    public void OnEventRecieved(string eventData)
    {
		var dataContainer = JsonConvert.DeserializeObject<MessageDataContainer>(eventData);
		AddToMessageHistory(false, dataContainer);
    }
}