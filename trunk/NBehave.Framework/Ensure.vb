Option Strict On

Imports System.Threading.Thread
Imports NBehave.Framework.World
Imports NBehave.Framework.Scenario


Public Interface IIsSomething
    Sub IsTrue(ByVal expected As Boolean)
    Sub IsFalse(ByVal expected As Boolean)
    'TODO: Add some more nice verifications...
End Interface


Public Class EnsureBase(Of T)
    Implements IIsSomething

    Protected _outcome As Outcome ' World.IWorldOutcome(Of T)


    Public Sub IsFalse(ByVal expected As Boolean) Implements IIsSomething.IsFalse
        Me.Expected(expected, False)
    End Sub

    Public Sub IsTrue(ByVal expected As Boolean) Implements IIsSomething.IsTrue
        Me.Expected(expected, True)
    End Sub

    Private Sub Expected(ByVal expected As Boolean, ByVal compareTo As Boolean)
        _outcome.Message = String.Format(CurrentThread.CurrentUICulture, "Expected {0}, but is {1}", compareTo.ToString, expected.ToString)
        _outcome.Passed = (expected = compareTo)
    End Sub

End Class



Public Class Ensure(Of T)
    Inherits EnsureBase(Of T)

    'Added 30/7
    Public Sub New(ByVal outcome As Outcome)
        _outcome = outcome
    End Sub

    Public Sub New(ByVal outcome As IWorldOutcome(Of T))
        _outcome = outcome.Result
    End Sub

    Public Sub Failure()
        Failure(New Outcome(False, "Failure"))
    End Sub

    Public Sub Failure(ByVal theFailure As Outcome)
        _outcome = theFailure
    End Sub

    Public ReadOnly Property Outcome() As Outcome
        Get
            Return _outcome
        End Get
    End Property

End Class
