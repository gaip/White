using System.Windows;
using System.Windows.Automation;
using TestStack.White.UIItems.Actions;
using Action = TestStack.White.UIItems.Actions.Action;

namespace TestStack.White.UIItems.Scrolling
{
    [PlatformSpecificItem(ReferAsType = typeof (IVScrollBar))]
    public class WpfVScrollBar : WpfScrollBar, IVScrollBar
    {
        private readonly ActionListener actionListener;

        public WpfVScrollBar(AutomationElement parent, ActionListener actionListener) : base(parent)
        {
            this.actionListener = actionListener;
        }

        protected override double ScrollPercentage
        {
            get { return ScrollPattern.Current.VerticalViewSize; }
        }

        public override double Value
        {
            get { return ScrollPattern.Current.VerticalScrollPercent; }
        }

        public override Rect Bounds
        {
            get { return Rect.Empty; }
        }

        public virtual void ScrollUp()
        {
            Scroll(ScrollAmount.SmallDecrement);
        }

        public virtual void ScrollDown()
        {
            Scroll(ScrollAmount.SmallIncrement);
        }

        public virtual void ScrollUpLarge()
        {
            Scroll(ScrollAmount.LargeDecrement);
        }

        public virtual void ScrollDownLarge()
        {
            Scroll(ScrollAmount.LargeIncrement);
        }

        public virtual bool IsScrollable
        {
            get { return ScrollPattern.Current.VerticallyScrollable; }
        }

        public virtual bool IsNotMinimum
        {
            get { return Value > 0; }
        }

        public override void SetToMinimum()
        {
            SetToMinimum(ScrollUpLarge);
        }

        public override void SetToMaximum()
        {
            SetToMaximum(ScrollDownLarge);
        }

        protected virtual void Scroll(ScrollAmount amount)
        {
            if (!IsScrollable) return;
            switch (amount)
            {
                case ScrollAmount.LargeDecrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.Current.HorizontalScrollPercent, 
                        ValidPercentage(ScrollPattern.Current.VerticalScrollPercent - ScrollPercentage));
                    break;
                case ScrollAmount.SmallDecrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.Current.HorizontalScrollPercent,
                        ValidPercentage(ScrollPattern.Current.VerticalScrollPercent - SmallPercentage()));
                    break;
                case ScrollAmount.LargeIncrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.Current.HorizontalScrollPercent, 
                        ValidPercentage(ScrollPattern.Current.VerticalScrollPercent + ScrollPercentage));
                    break;
                case ScrollAmount.SmallIncrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.Current.HorizontalScrollPercent,
                        ValidPercentage(ScrollPattern.Current.VerticalScrollPercent + SmallPercentage()));
                    break;
            }
            actionListener.ActionPerformed(Action.Scroll);
        }
    }
}
