using Microsoft.Maui.Controls.Shapes;

namespace Aiyo_Client.Controls;

public partial class MessageBubble : ContentView
{
    //Mine
	private static CornerRadius myRadius;
    private const LayoutAlignment myLayoutAlignment = LayoutAlignment.End;

    //Others
    private static CornerRadius othersRadius;
    private const LayoutAlignment othersLayoutAlignment = LayoutAlignment.Start;

    public MessageBubble(bool mine)
	{
        InitializeComponent();
        SetupLayout(mine);
    }

    private void TryInitUIOptions()
    {
        if (othersRadius == default)
            othersRadius = new CornerRadius(10, 10, 0, 10);

        if (myRadius == default)
            myRadius = new CornerRadius(10, 10, 10, 0);
    }

    private void SetupLayout(bool isMine)
    {
        TryInitUIOptions();
        //left or right alignment of message
        var bubbleLayoutOptions = parentLayout.HorizontalOptions;
        bubbleLayoutOptions.Alignment = isMine ? myLayoutAlignment : othersLayoutAlignment;
        parentLayout.HorizontalOptions = bubbleLayoutOptions;

        //left or right alignment of time text
        var timeTextLayoutOptions = timeText.HorizontalOptions;
        timeTextLayoutOptions.Alignment = isMine ? myLayoutAlignment : othersLayoutAlignment;
        timeText.HorizontalOptions = timeTextLayoutOptions;

        //setting corner radius of bubble
        strokeShape.CornerRadius = isMine ? myRadius : othersRadius;    
    }

    public void SetText(string text)
    {
        messageText.Text = text;
    }

    public void SetTime(long unixTimeSeconds)
    {
        timeText.Text = DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds).ToLocalTime().ToString("HH:mm");
    }
}