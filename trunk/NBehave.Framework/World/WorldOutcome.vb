Option Strict On

Imports NBehave.Framework.Scenario


Namespace World


    Public MustInherit Class WorldOutcome
        Implements IWorldOutcome


        Private _result As Outcome = New Outcome(OutcomeResult.Failed, "World not Verified")    ' Set a default result
        Private _ensurer As Ensure = New Ensure(Me)

        Protected MustOverride Sub Verify(Of T)(ByVal world As T) Implements IWorldOutcome.Verify


        Protected Sub New()
        End Sub


        Public ReadOnly Property Ensure() As Ensure
            Get
                Return _ensurer
            End Get
        End Property


        Private Property Result() As Outcome Implements IWorldOutcome.Result
            Get
                Return _result
            End Get
            Set(ByVal Value As Outcome)
                _result = Value
            End Set
        End Property


    End Class


End Namespace
