using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace ElectricPlayer.Player.Controls;

public class SeekSlider : RangeBase
{
    public static readonly DirectProperty<SeekSlider, double> SeekValueProperty =
        AvaloniaProperty.RegisterDirect<SeekSlider, double>(
            nameof(SeekValue),
            o => o.SeekValue,
            (o, v) => o.SeekValue = v);

    public static readonly DirectProperty<SeekSlider, bool> IsSeekingProperty =
        AvaloniaProperty.RegisterDirect<SeekSlider, bool>(
            nameof(IsSeeking),
            o => o.IsSeeking,
            (o, v) => o.IsSeeking = v);
    
    private bool _IsSeeking;
    private double _SeekValue;
    
    public double SeekValue
    {
        get => _SeekValue;
        set => SetAndRaise(SeekValueProperty, ref _SeekValue, value);
    }

    public bool IsSeeking
    {
        get => _IsSeeking;
        set => SetAndRaise(IsSeekingProperty, ref _IsSeeking, value);
    }
    
    private Track _track;
    private Button _decreaseButton;
    private Button _increaseButton;

    static SeekSlider()
    {
        Thumb.DragStartedEvent.AddClassHandler<SeekSlider>((x, e) => x.OnThumbDragStarted(e),
            RoutingStrategies.Bubble);
        Thumb.DragDeltaEvent.AddClassHandler<SeekSlider>((x, e) => x.OnThumbDragDelta(e), RoutingStrategies.Bubble);
        Thumb.DragCompletedEvent.AddClassHandler<SeekSlider>((x, e) => x.OnThumbDragCompleted(e),
            RoutingStrategies.Bubble);
    }
    
    protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
    {
        _decreaseButton = e.NameScope.Find<Button>("PART_DecreaseButton");
        _track = e.NameScope.Find<Track>("PART_Track");
        _increaseButton = e.NameScope.Find<Button>("PART_IncreaseButton");

        if (_decreaseButton != null) _decreaseButton.PointerPressed += DC_PP;

        if (_increaseButton != null) _increaseButton.PointerPressed += IC_PP;
    }
    
    private void IC_PP(object sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) return;
        var x = e.GetCurrentPoint(_track);
        SeekValue = x.Position.X / _track.Bounds.Width;
    }

    private void DC_PP(object sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) return;
        var x = e.GetCurrentPoint(_track);
        SeekValue = x.Position.X / _track.Bounds.Width;
    }
    
    protected virtual void OnThumbDragStarted(VectorEventArgs e)
    {
        IsSeeking = true;
    }
    
    protected virtual void OnThumbDragDelta(VectorEventArgs e)
    {
        if (e.Source is Thumb thumb && _track?.Thumb == thumb) MoveToNextTick(_track.Value);
    }
    
    protected virtual void OnThumbDragCompleted(VectorEventArgs e)
    {
        SeekValue = Value;
        IsSeeking = false;
    }
    
    private void MoveToNextTick(double value)
    {
        Value = Math.Max(Minimum, Math.Min(Maximum, value));
    }
}