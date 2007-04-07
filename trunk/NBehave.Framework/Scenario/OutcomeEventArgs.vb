Option Strict On

Imports System.Collections.ObjectModel


Namespace Scenario

    Public Class OutcomeEventArgs
        Inherits EventArgs

        Private _outcomes As ReadOnlyCollection(Of ScenarioOutcome)


        Public Sub New(ByVal outcomes As ReadOnlyCollection(Of ScenarioOutcome))
            Me._outcomes = outcomes
        End Sub


        Public ReadOnly Property Outcomes() As ReadOnlyCollection(Of ScenarioOutcome)
            Get
                Return _outcomes
            End Get
        End Property


    End Class

End Namespace