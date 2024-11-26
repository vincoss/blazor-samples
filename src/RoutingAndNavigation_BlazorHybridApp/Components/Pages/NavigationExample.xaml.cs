namespace BlazorRoutingAndNavigation_MauiBlazorHybridApp.Components.Pages;

public partial class NavigationExample : ContentPage
{
	public NavigationExample()
	{
		InitializeComponent();
	}

    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}