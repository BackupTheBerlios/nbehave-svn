Option Strict On

Imports NBehave.Framework.Scenario


Namespace World

    Public Interface IWorldOutcome
        Property Result() As Outcome
        Sub Verify(Of T)(ByVal world As T)
    End Interface


End Namespace
