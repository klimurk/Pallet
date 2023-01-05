using System.Timers;
using System.Windows.Controls;
using Timer = System.Timers.Timer;

namespace Pallet.Infrastructure.Common
{
    internal class ToolTipHelper
    {
        private readonly ToolTip _toolTip;
        private readonly Timer _timer;

        /// <summary>
        /// Creates an instance
        /// </summary>
        public ToolTipHelper()
        {
            _toolTip = new ToolTip();
            _timer = new Timer { AutoReset = false };
            _timer.Elapsed += ShowToolTip;
        }

        /// <summary>
        /// Gets or sets the text for the tooltip.
        /// </summary>
        public object ToolTipContent
        { get { return _toolTip.Content; } set { _toolTip.Content = value; } }

        /// <summary>
        /// To be called when the mouse enters the ui area.
        /// </summary>
        public void OnMouseEnter(object sender, MouseEventArgs e)
        {
            _timer.Interval = ToolTipService.GetInitialShowDelay(Application.Current.MainWindow);
            _timer.Start();
        }

        private void ShowToolTip(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            if (_toolTip != null)
                _toolTip.Dispatcher.Invoke(new Action(() => { _toolTip.IsOpen = true; }));
        }

        /// <summary>
        /// To be called when the mouse leaves the ui area.
        /// </summary>
        public void OnMouseLeave(object sender, MouseEventArgs e)
        {
            _timer.Stop();
            if (_toolTip != null)
                _toolTip.IsOpen = false;
        }
    }
}