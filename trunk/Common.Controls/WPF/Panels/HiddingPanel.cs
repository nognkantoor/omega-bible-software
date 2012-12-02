using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Common.Controls.WPF;
using System.Windows.Media;
using Common.Core.Error;
using System.ComponentModel;
using System.Timers;

namespace Common.Controls.WPF.Panels
{
    public class HiddingPanel : Expander
    {
        #region Exceptions

        public static readonly Exceptions NoSuitableParentException = Exceptions.Pool.Get("HiddingPanelNoParent")
                .Set("There was no parent found for the HiddingPanel which could be the mouse move holder.")
                .SetUserDescription("There was a problem in the application user interface initialization");

        #endregion Exceptions

        #region Private fields

        private object _lastHeader = null;
        private Timer _reactionTimer;
        private Point _lastPosition;
        private bool _lastInScope = false;

        #endregion Private fields

        #region Constructors

        public HiddingPanel()
        {
            _reactionTimer = new Timer();
            _reactionTimer.Elapsed += new ElapsedEventHandler(ReactionDelayElapsed);
        }

        #endregion Constructors

        #region Parent window

        /// <summary>
        /// Gets the FrameworkElement which tracks the mouse move event to find out if 
        /// it's getting close to the panel.
        /// </summary>
        protected FrameworkElement CapturingVisual
        {
            get;
            private set;
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            if(CapturingVisual != null)
            {
                CapturingVisual.PreviewMouseMove -= CapturingVisualPreviewMouseMove;
            }
            CapturingVisual = this.GetLastParent<FrameworkElement>();
            if (CapturingVisual == null)
            {
                WrapperException.Throw(NoSuitableParentException);
            }

            if (CapturingVisual != null)
            {
                CapturingVisual.PreviewMouseMove += CapturingVisualPreviewMouseMove;
            }
        }

        private void CapturingVisualPreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _lastPosition = e.GetPosition(this);
            bool tmp = _lastInScope;
            _lastInScope = CheckIfInScope();

            if (_lastInScope)
            {
                if (ReactionDelay.TotalMilliseconds != 0)
                {
                    if (!tmp)
                    {
                        _reactionTimer.Interval = ReactionDelay.TotalMilliseconds;
                        _reactionTimer.Start();
                    }
                }
                else if (IsExpanded != true)
                {
                    IsExpanded = true;
                }
            }
            else
            {
                if (ReactionDelay.TotalMilliseconds == 0 && IsExpanded != false && !IsFocused)
                {
                    IsExpanded = false;
                }
            }
#if TEST
            //if (_lastHeader == null) _lastHeader = Header != null ? Header : string.Empty;
            //Header = _lastHeader.ToString() + string.Format(" ({0},{1})", _lastPosition.X, _lastPosition.Y);
            //Background = IsExpanded ? Brushes.Green : Brushes.Red;
#endif
        }

        private bool CheckIfInScope()
        {
            return _lastPosition.X > -ShowPanelRegion.Left && _lastPosition.X < this.ActualWidth + ShowPanelRegion.Right
                && _lastPosition.Y > -ShowPanelRegion.Top && _lastPosition.Y < this.ActualHeight + ShowPanelRegion.Bottom;
        }

        void ReactionDelayElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => IsExpanded = _lastInScope));
        }

        #endregion Parent window

        #region Appearance settings
        
        /// <summary>
        /// Gets or sets the region, where the panel should show up, when the mouse moves into it.  
        /// </summary>
        public Thickness ShowPanelRegion
        {
            get { return (Thickness)GetValue(ShowPanelRegionProperty); }
            set { SetValue(ShowPanelRegionProperty, value); }
        }

        /// <summary>
        /// Holds the dependency property of the region, where the panel should show up, when the mouse moves into it.  
        /// </summary>
        public static readonly DependencyProperty ShowPanelRegionProperty =
            DependencyProperty.Register("ShowPanelRegion", typeof(Thickness), typeof(HiddingPanel), new UIPropertyMetadata(new Thickness(10)));



        public TimeSpan ReactionDelay
        {
            get { return (TimeSpan)GetValue(ReactionDelayProperty); }
            set { SetValue(ReactionDelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReactionDelay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReactionDelayProperty =
            DependencyProperty.Register("ReactionDelay", typeof(TimeSpan), typeof(HiddingPanel), new UIPropertyMetadata(new TimeSpan(0)));

        

        #endregion Appearance settings
        
    }
}
