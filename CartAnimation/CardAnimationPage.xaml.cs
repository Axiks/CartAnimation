using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CartAnimation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CardAnimationPage : Page
    {
        public CardAnimationPage()
        {
            this.InitializeComponent();

            movingImage.RenderTransform = new CompositeTransform();
            SetMoveTime();

            StartAnimation();
        }

        private bool IsMove = true;
        private bool IsForwardDirection = false;
        private int TimeOfTravel = 4;

        private void StartAnimation()
        {
            if (IsForwardDirection)
            {
                moveStoryboard.Begin();
            }
            else
            {
                reverseStoryboard.Begin();
            }
        }

        private void AccidentBtnClick(object sender, RoutedEventArgs e)
        {
            if (IsMove)
            {
                Pause();
            }
            else
            {
                Resume();
            }
            IsMove = !IsMove;
        }

        private void Resume()
        {
            moveStoryboard.Resume();
            reverseStoryboard.Resume();
        }

        private void Pause()
        {
            moveStoryboard.Pause();
            reverseStoryboard.Pause();
        }

        private void SetMoveTime()
        {
            foreach (var timeline in moveStoryboard.Children)
            {
                if (timeline is DoubleAnimation)
                {
                    DoubleAnimation yourDoubleAnimation = timeline as DoubleAnimation;
                    var timeSpan = TimeSpan.FromSeconds(TimeOfTravel);
                    yourDoubleAnimation.Duration = new Duration(timeSpan);
                }
            }
            foreach (var timeline in reverseStoryboard.Children)
            {
                if (timeline is DoubleAnimation)
                {
                    DoubleAnimation yourDoubleAnimation = timeline as DoubleAnimation;
                    var timeSpan = TimeSpan.FromSeconds(TimeOfTravel);
                    yourDoubleAnimation.Duration = new Duration(timeSpan);
                }
            }
        }
    }
}
