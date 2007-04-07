Option Strict On

Imports NBehave.Framework.Scenario


Namespace World


    Public MustInherit Class WorldOutcome(Of T)
        Implements IWorldOutcome(Of T)


        Private _result As ScenarioOutcome = New ScenarioOutcome(False, "World not Verified")    ' Set a default result
        Private _ensurer As Ensure(Of T) = New Ensure(Of T)(Me)

        Protected MustOverride Sub Verify(ByVal world As T) Implements IWorldOutcome(Of T).Verify


        Protected Sub New()
        End Sub


        Public ReadOnly Property Ensure() As Ensure(Of T)
            Get
                Return _ensurer
            End Get
        End Property


        Private Property Result() As ScenarioOutcome Implements IWorldOutcome(Of T).Result
            Get
                Return _result
            End Get
            Set(ByVal Value As ScenarioOutcome)
                _result = Value
            End Set
        End Property


    End Class


End Namespace
