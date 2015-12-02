using System;
using Xunit;

namespace WCFBowlingGameService.UnitTests
{
    public class WCFBowlingGameTest : BowlingGameTest, IDisposable
    {
        public WCFBowlingGameTest()
        {
            WcfServiceHost.StartService<BowlingGame, IBowlingGame>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                WcfServiceHost.StopService<IBowlingGame>();
            }
        }

        private void ExecuteWithService(Action action)
        {
            WcfServiceHost.InvokeService<IBowlingGame>(service =>
            {
                this.game = service;
                action.Invoke();
            });
        }

        [Fact]
        public override void AllOnes()
        {
            ExecuteWithService(base.AllOnes);
        }

        [Fact]
        public override void GutterGame()
        {
            ExecuteWithService(base.GutterGame);
        }

        [Fact]
        public override void OneSpace()
        {
            ExecuteWithService(base.OneSpace);
        }

        [Fact]
        public override void OneStrike()
        {
            ExecuteWithService(base.OneStrike);
        }

        [Fact]
        public override void PerfectGame()
        {
            ExecuteWithService(base.PerfectGame);
        }
    }
}
