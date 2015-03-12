using System;
using System.ServiceModel;

namespace WCFBowlingGameService
{
    [ServiceContract(Name="BowlingGame", Namespace="http://bowlinggame/services")]
    public interface IBowlingGame
    {
        [OperationContract()]
        void Roll(int pins);
        [OperationContract()]
        int Score();
    }
}
