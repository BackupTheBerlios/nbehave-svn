Option Strict On

Imports System.Threading.Thread
Imports NBehave.Framework.World
Imports NBehave.Framework.Scenario


Public Interface IIsSomething
    Sub IsTrue(ByVal expected As Boolean)
    Sub IsFalse(ByVal expected As Boolean)
    'TODO: Add some more nice verifications...
End Interface


Public Class Ensure(Of T)
    Implements IIsSomething


    Private _outcome As World.IWorldOutcome(Of T)

    Public Sub New(ByVal outcome As IWorldOutcome(Of T))
        _outcome = outcome
    End Sub

    Public Sub Failure()
        Failure(New ScenarioOutcome(False, "Failure"))
    End Sub

    Public Sub Failure(ByVal theFailure As ScenarioOutcome)
        _outcome.Result = theFailure
    End Sub

    Public ReadOnly Property Outcome() As World.IWorldOutcome(Of T)
        Get
            Return _outcome
        End Get
    End Property


    Public Sub IsFalse(ByVal expected As Boolean) Implements IIsSomething.IsFalse
        _outcome.Result = New ScenarioOutcome(expected = False, String.Format(CurrentThread.CurrentUICulture, "Expected False, but is {0}", expected.ToString))
    End Sub

    Public Sub IsTrue(ByVal expected As Boolean) Implements IIsSomething.IsTrue
        _outcome.Result = New ScenarioOutcome(expected = True, String.Format(CurrentThread.CurrentUICulture, "Expected True, but is {0}", expected.ToString))
    End Sub

End Class
