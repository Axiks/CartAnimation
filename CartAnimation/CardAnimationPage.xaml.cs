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
using System.Configuration;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CartAnimation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CardAnimationPage : Page
    {
        private CompositeTransform _compositeTransform;
        private bool IsMove = false;
        private bool IsForwardDirection;

        private Duration animationDuration;

        public CardAnimationPage()
        {
            this.InitializeComponent();

            IsForwardDirection = bool.Parse(ConfigurationManager.AppSettings["animation_is_forward_direction"]);
            var AnimationDuration = int.Parse(ConfigurationManager.AppSettings["animation_duration"]);
            animationDuration = new Duration(TimeSpan.FromSeconds(AnimationDuration));

            _compositeTransform = new CompositeTransform();
            InitStartPosition(150, 150);
            movingImage.RenderTransform = _compositeTransform;
        }

        private void StartAnimation()
        {
            IsMove = true;
            if (IsForwardDirection)
            {
                moveStoryboard.Begin();
                moveStoryboard.Completed += (s, e) =>
                {
                    reverseStoryboard.Begin();
                };
            }
            else
            {
                reverseStoryboard.Begin();
                reverseStoryboard.Completed += (s, e) =>
                {
                    moveStoryboard.Begin();
                };
            }
        }

        private void MoveBtn_Click(object sender, RoutedEventArgs e) => StartAnimation();

        private void AccidentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsMove)
            {
                PauseAnimation();
            }
            else
            {
                ResumeAnimation();
            }
            IsMove = !IsMove;
        }

        private void ResumeAnimation()
        {
            moveStoryboard.Resume();
            reverseStoryboard.Resume();
        }

        private void PauseAnimation()
        {
            moveStoryboard.Pause();
            reverseStoryboard.Pause();
        }

        private void InitStartPosition(int x, int y)
        {
            if (IsForwardDirection)
            {
                _compositeTransform.TranslateX = x;
                _compositeTransform.TranslateY = y;
            }
            else
            {
                _compositeTransform.TranslateX = -x;
                _compositeTransform.TranslateY = -y;
            }
        }
    }
}
