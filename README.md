# FlexButton
Flexible button control for Xamarin.Forms.

<!--
[![NuGet](https://img.shields.io/nuget/v/Forms.Controls.SwipeCards.svg?label=NuGet&style=flat-square)](https://www.nuget.org/packages/Forms.Controls.SwipeCards/)-->

## How to use
**Add the [NuGet package](https://www.nuget.org/packages/Forms.Controls.SwipeCards/) to the Xamarin.Forms project**
```
PM> Install-Package Forms.Controls.Flexbutton
```

**[iOS only] Initialize the control in your *AppDelegate.cs*.**
```csharp
FlexButton.Init(); // Call this after Forms.Init();
```


**Add the XML namespace**
```xml
xmlns:flex="clr-namespace:Flex.Controls;assembly=Flex"
```

**Add the control**
```xml
<flex:FlexButton
    WidthRequest="76"
    HeightRequest="76"
    CornerRadius="38"
    Icon="lightbulb.png"
    IconColor="#ffffff"
    HighlightIconColor="#49516F"
    BackgroundColor="#6279B8"
    HighlightBackgroundColor="#8EA4D2"
    TouchedDown="DemoButton_TouchedDown"
    TouchedUp="DemoButton_TouchedUp"/>
```
## Preview
![Preview](/Design/Swipecards.gif)

## API Reference
| Property | Default | Description |
|------------------|---------|-------------|
| CardMoveDistance | null | How far the card has to be dragged to trigger the swipe. Default is 30% of the control |

| Command | Parameter | Description |
|------------------|---------|-------------|
| SwipedLeftCommand | Selected Item | Triggered, when card got swiped to the left |
| SwipedRightCommand | Selected Item | Triggered, when card got swiped to the right |

| Action | Parameter | Description |
|------------------|---------|-------------|
| SwipedLeft | Selected Item | Triggered, when card got swiped to the left |
| SwipedRight | Selected Item | Triggered, when card got swiped to the right |
| StartedDragging | Selected Item | Triggered, when card got dragged |
| FinishedDragging | Selected Item | Triggered, when dragging finished |
