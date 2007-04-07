Option Strict On

Imports NBehave.Framework.Scenario


Namespace World

    Public Interface IWorldOutcome(Of T)
        Property Result() As ScenarioOutcome
        Sub Verify(ByVal world As T)
    End Interface


End Namespace
