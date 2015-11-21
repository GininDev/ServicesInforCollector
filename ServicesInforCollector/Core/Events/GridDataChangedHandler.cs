using System;

namespace ServicesInforCollector.Core.Events
{
    public delegate void GridDataChangedHandler(object sender, GridDataChangedHandlerArgs e);

    public class GridDataChangedHandlerArgs : EventArgs
    {
        private readonly int _reached;

        public GridDataChangedHandlerArgs(int num)
        {
            _reached = num;
        }

        public int ReachedNumber
        {
            get { return _reached; }
        }
    }
}